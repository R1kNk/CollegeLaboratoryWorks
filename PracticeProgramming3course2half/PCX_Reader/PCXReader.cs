using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace PCX_Reader
{
    public class PCXReader
    {

        public static Bitmap Load(string fileName)
        {
            Bitmap bmp = null;
            using (var f = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                bmp = Load(f);
            }
            return bmp;
        }


        public static Bitmap Load(Stream stream)
        {
            int imgWidth = -1;
            int imgHeight = -1;
            int imgBpp = -1;
            Bitmap theBitmap = null;
            BinaryReader reader = new BinaryReader(stream);

            byte tempByte = (byte)stream.ReadByte();
            if (tempByte != 10)
                throw new ApplicationException("This is not a  PCX file.");

            tempByte = (byte)stream.ReadByte();
            if (tempByte < 0 || tempByte > 5)
                throw new ApplicationException("Invalid PCX version.");

            tempByte = (byte)stream.ReadByte();
            if (tempByte != 1)
                throw new ApplicationException("Invalid encoding char.");

            imgBpp = stream.ReadByte();


            UInt16 xmin = reader.ReadUInt16();
            UInt16 ymin = reader.ReadUInt16();
            UInt16 xmax = reader.ReadUInt16();
            UInt16 ymax = reader.ReadUInt16();

            imgWidth = xmax - xmin + 1;
            imgHeight = ymax - ymin + 1;

            if ((imgWidth < 1) || (imgHeight < 1) || (imgWidth > 32767) || (imgHeight > 32767))
                throw new ApplicationException("This PCX file appears to have invalid dimensions.");

            reader.ReadUInt16(); //horiontal resolution
            reader.ReadUInt16(); //vertical resolution

            byte[] colorPalette = new byte[48];
            stream.Read(colorPalette, 0, 48);

            stream.ReadByte();

            int numPlanes = stream.ReadByte();
            int bytesPerLine = reader.ReadUInt16();
            if (bytesPerLine == 0) bytesPerLine = xmax - xmin + 1;

            if (imgBpp == 8 && numPlanes == 1)
            {
                colorPalette = new byte[768];
                stream.Seek(-768, SeekOrigin.End);
                stream.Read(colorPalette, 0, 768);
            }

            //fix color palette if it's a 1-bit image, and there's no palette information
            if (imgBpp == 1)
            {
                if ((colorPalette[0] == colorPalette[3]) && (colorPalette[1] == colorPalette[4]) && (colorPalette[2] == colorPalette[5]))
                {
                    colorPalette[0] = colorPalette[1] = colorPalette[2] = 0;
                    colorPalette[3] = colorPalette[4] = colorPalette[5] = 0xFF;
                }
            }

            byte[] bmpData = new byte[(imgWidth + 1) * 4 * imgHeight];
            stream.Seek(128, SeekOrigin.Begin);
            int x = 0, y = 0, i, j;

            RleReader rleReader = new RleReader(stream);

            try
            {
                if (imgBpp == 1)
                {
                    int b, p;
                    byte val;
                    byte[] scanline = new byte[bytesPerLine];
                    byte[] realscanline = new byte[bytesPerLine * 8];

                    for (y = 0; y < imgHeight; y++)
                    {
                        //add together all the planes...
                        Array.Clear(realscanline, 0, realscanline.Length);
                        for (p = 0; p < numPlanes; p++)
                        {
                            x = 0;
                            for (i = 0; i < bytesPerLine; i++)
                            {
                                scanline[i] = (byte)rleReader.ReadByte();

                                for (b = 7; b >= 0; b--)
                                {
                                    if ((scanline[i] & (1 << b)) != 0) val = 1; else val = 0;
                                    realscanline[x] |= (byte)(val << p);
                                    x++;
                                }
                            }
                        }

                        for (x = 0; x < imgWidth; x++)
                        {
                            i = realscanline[x];
                            bmpData[4 * (y * imgWidth + x)] = colorPalette[i * 3 + 2];
                            bmpData[4 * (y * imgWidth + x) + 1] = colorPalette[i * 3 + 1];
                            bmpData[4 * (y * imgWidth + x) + 2] = colorPalette[i * 3];
                        }
                    }
                }
                else
                {
                    if (numPlanes == 1)
                    {
                        if (imgBpp == 8)
                        {
                            byte[] scanline = new byte[bytesPerLine];
                            for (y = 0; y < imgHeight; y++)
                            {
                                for (i = 0; i < bytesPerLine; i++)
                                    scanline[i] = (byte)rleReader.ReadByte();

                                for (x = 0; x < imgWidth; x++)
                                {
                                    i = scanline[x];
                                    bmpData[4 * (y * imgWidth + x)] = colorPalette[i * 3 + 2];
                                    bmpData[4 * (y * imgWidth + x) + 1] = colorPalette[i * 3 + 1];
                                    bmpData[4 * (y * imgWidth + x) + 2] = colorPalette[i * 3];
                                }
                            }
                        }
                        else if (imgBpp == 4)
                        {
                            byte[] scanline = new byte[bytesPerLine];
                            for (y = 0; y < imgHeight; y++)
                            {
                                for (i = 0; i < bytesPerLine; i++)
                                    scanline[i] = (byte)rleReader.ReadByte();

                                for (x = 0; x < imgWidth; x++)
                                {
                                    i = scanline[x / 2];
                                    bmpData[4 * (y * imgWidth + x)] = colorPalette[((i >> 4) & 0xF) * 3 + 2];
                                    bmpData[4 * (y * imgWidth + x) + 1] = colorPalette[((i >> 4) & 0xF) * 3 + 1];
                                    bmpData[4 * (y * imgWidth + x) + 2] = colorPalette[((i >> 4) & 0xF) * 3];
                                    x++;
                                    bmpData[4 * (y * imgWidth + x)] = colorPalette[(i & 0xF) * 3 + 2];
                                    bmpData[4 * (y * imgWidth + x) + 1] = colorPalette[(i & 0xF) * 3 + 1];
                                    bmpData[4 * (y * imgWidth + x) + 2] = colorPalette[(i & 0xF) * 3];
                                }
                            }
                        }
                        else if (imgBpp == 2)
                        {
                            byte[] scanline = new byte[bytesPerLine];
                            for (y = 0; y < imgHeight; y++)
                            {
                                for (i = 0; i < bytesPerLine; i++)
                                    scanline[i] = (byte)rleReader.ReadByte();

                                for (x = 0; x < imgWidth; x++)
                                {
                                    i = scanline[x / 4];
                                    bmpData[4 * (y * imgWidth + x)] = colorPalette[((i >> 6) & 0x3) * 3 + 2];
                                    bmpData[4 * (y * imgWidth + x) + 1] = colorPalette[((i >> 6) & 0x3) * 3 + 1];
                                    bmpData[4 * (y * imgWidth + x) + 2] = colorPalette[((i >> 6) & 0x3) * 3];
                                    x++;
                                    bmpData[4 * (y * imgWidth + x)] = colorPalette[((i >> 4) & 0x3) * 3 + 2];
                                    bmpData[4 * (y * imgWidth + x) + 1] = colorPalette[((i >> 4) & 0x3) * 3 + 1];
                                    bmpData[4 * (y * imgWidth + x) + 2] = colorPalette[((i >> 4) & 0x3) * 3];
                                    x++;
                                    bmpData[4 * (y * imgWidth + x)] = colorPalette[((i >> 2) & 0x3) * 3 + 2];
                                    bmpData[4 * (y * imgWidth + x) + 1] = colorPalette[((i >> 2) & 0x3) * 3 + 1];
                                    bmpData[4 * (y * imgWidth + x) + 2] = colorPalette[((i >> 2) & 0x3) * 3];
                                    x++;
                                    bmpData[4 * (y * imgWidth + x)] = colorPalette[(i & 0x3) * 3 + 2];
                                    bmpData[4 * (y * imgWidth + x) + 1] = colorPalette[(i & 0x3) * 3 + 1];
                                    bmpData[4 * (y * imgWidth + x) + 2] = colorPalette[(i & 0x3) * 3];
                                }
                            }
                        }
                    }
                    else if (numPlanes == 3)
                    {
                        byte[] scanlineR = new byte[bytesPerLine];
                        byte[] scanlineG = new byte[bytesPerLine];
                        byte[] scanlineB = new byte[bytesPerLine];
                        int bytePtr = 0;

                        for (y = 0; y < imgHeight; y++)
                        {
                            for (i = 0; i < bytesPerLine; i++)
                                scanlineR[i] = (byte)rleReader.ReadByte();
                            for (i = 0; i < bytesPerLine; i++)
                                scanlineG[i] = (byte)rleReader.ReadByte();
                            for (i = 0; i < bytesPerLine; i++)
                                scanlineB[i] = (byte)rleReader.ReadByte();

                            for (int n = 0; n < imgWidth; n++)
                            {
                                bmpData[bytePtr++] = scanlineB[n];
                                bmpData[bytePtr++] = scanlineG[n];
                                bmpData[bytePtr++] = scanlineR[n];
                                bytePtr++;
                            }
                        }
                    }

                }//bpp

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Error while processing PCX file: " + e.Message);
            }

            theBitmap = new Bitmap((int)imgWidth, (int)imgHeight, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            System.Drawing.Imaging.BitmapData bmpBits = theBitmap.LockBits(new Rectangle(0, 0, theBitmap.Width, theBitmap.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppRgb);
            System.Runtime.InteropServices.Marshal.Copy(bmpData, 0, bmpBits.Scan0, imgWidth * 4 * imgHeight);
            theBitmap.UnlockBits(bmpBits);
            return theBitmap;
        }


        /// <summary>
        /// Helper class for reading a run-length encoded stream in a PCX file.
        /// </summary>
        private class RleReader
        {
            private int currentByte = 0;
            private int runLength = 0, runIndex = 0;
            private Stream stream;

            public RleReader(Stream stream)
            {
                this.stream = stream;
            }

            public int ReadByte()
            {
                if (runLength > 0)
                {
                    runIndex++;
                    if (runIndex == (runLength - 1))
                        runLength = 0;
                }
                else
                {
                    currentByte = stream.ReadByte();
                    if (currentByte > 191)
                    {
                        runLength = currentByte - 192;
                        currentByte = stream.ReadByte();
                        if (runLength == 1)
                            runLength = 0;
                        runIndex = 0;
                    }
                }
                return currentByte;
            }
        }




    }
}
