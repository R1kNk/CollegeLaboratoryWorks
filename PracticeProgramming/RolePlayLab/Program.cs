using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Threading;

namespace RolePlayLab
{
    delegate uint ChooseDelegate(uint CurrentId);
    interface IMagic
    {
        void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1);
    }
    class RolePlayCharacter : IComparable<RolePlayCharacter>
    {

        public static List<uint> UnicNumbersList = new List<uint>();
        public static ArrayList StatesForCharacter = new ArrayList() { "Здоров", "Ослаблен", "Болен", "Отравлен", "Парализован", "Мертв" };
        //margins
        private uint unicCharacterNumber;
        private string nameCharacter;
        protected string stateCharacter;
        protected bool canSpeak;
        protected bool canMove;
        private string raceCharacter;
        private string genderCharacter;
        protected uint currentHP;
        private uint maxHP;
        protected uint expCharacter;
        private uint ageCharacter;
        protected bool isSickCharacter;
        protected bool isPoisonedCharacter;
        protected bool isParalyzedCharacter;
        private bool isInvulnerable;
        //
        //properties
        public uint UnicCharacterNumber { get => unicCharacterNumber;  }
        public string NameCharacter { get => nameCharacter; }
        public string StateCharacter { get => stateCharacter; set => stateCharacter = value; }
        public bool CanSpeak { get => canSpeak; set => canSpeak = value; }
        public bool CanMove { get => canMove; set => canMove = value; }
        public string RaceCharacter { get => raceCharacter; }
        public string GenderCharacter { get => genderCharacter; }
        public uint CurrentHP { get => currentHP; set => currentHP = value; }
        public uint MaxHP { get => maxHP; private set => maxHP = value; }
        public uint ExpCharacter { get => expCharacter;  set => expCharacter = value; }
        public double PercentsHP { get => Convert.ToDouble((CurrentHP / (double)MaxHP) * 100.0); }
        public uint AgeCharacter { get => ageCharacter; protected set => ageCharacter = value; }
        public bool IsSickCharacter { get => isSickCharacter; set => isSickCharacter = value; }
        public bool IsPoisonedCharacter { get => isPoisonedCharacter; set => isPoisonedCharacter = value; }
        public bool IsParalyzedCharacter { get => isParalyzedCharacter; set => isParalyzedCharacter = value; }
        public bool IsInvulnerable { get => isInvulnerable; set => isInvulnerable = value; }

        //
        ArrayList Inventory = new ArrayList();

        //constructor

        public RolePlayCharacter()
        {
            bool ok = false;
            uint NumberChr = default(uint);
            while (!ok)
            {
                try
                {
                    Console.WriteLine("Введите уникальный индентификационный номер (больше нуля):");
                    NumberChr = Convert.ToUInt32(Console.ReadLine());
                    ok = true;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Число слишком большое,или меньше нуля. Попробуйте щеё раз");
                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Число введено некорректно. попробуйте ещё раз.");
                    continue;
                }
            }
            ok = false;
            string nameChr = default(string);
            ConsoleKeyInfo key;
            bool kk = false;
            while (!kk)
            {
                Console.WriteLine("0-Автоматический выбор имени\n1-Ввести имя");
                key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '0':
                        {
                            Random rand = new Random();
                            nameChr = (string)Program.CharacterNames[rand.Next(0, Program.CharacterNames.Count - 1)];
                            Console.WriteLine("\nВыбранное имя:{0}", nameChr);
                            kk = true;
                            break;
                        }
                    case '1': {
                            while (!ok)
                            {
                                Console.WriteLine("Введите имя персонажа:");
                                nameChr = Console.ReadLine();
                                ok = isNameCorrect(nameChr);
                                if (!ok) Console.WriteLine("Имя персонажа содержит запрещенные символы. попробуйте ещё раз.");
                            }
                            kk = true;
                            break;
                        }
                    default:
                        break;
                }
            }
           
            ok = false;
            string raceChr = ChooseRace();
            string gendChr = ChooseGender();
            uint agechr = default(uint);
            while (!ok)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("Введите возраст перснажа (больше нуля):");
                    agechr = Convert.ToUInt32(Console.ReadLine());
                    ok = true;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Число слишком большое,или меньше нуля. Попробуйте щеё раз");
                    continue;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Число введено некорректно. попробуйте ещё раз.");
                    continue;
                }

            }

            if (UnicNumbersList.Contains(NumberChr))
            {
                UnicNumbersList.Sort();
                Console.WriteLine("Указанный вами уникальный номер уже существует, номер будет выдан на 1 больше последнего существующего номера");
                unicCharacterNumber = UnicNumbersList.LastOrDefault() + 1;
                Console.WriteLine("Уникальный номер данного персонажа {0}", UnicCharacterNumber);
                UnicNumbersList.Add(unicCharacterNumber);
            }
            else { UnicNumbersList.Add(NumberChr); unicCharacterNumber = NumberChr; }
            nameCharacter = nameChr;
            raceCharacter = raceChr;
            genderCharacter = gendChr;
            ageCharacter = agechr;
            switch (RaceCharacter)
            {
                case "Человек": { MaxHP = 1000; CurrentHP = 1000; ExpCharacter = 0; break; }
                case "Гном": { MaxHP = 1500; CurrentHP = 1500; ExpCharacter = 0; break; }
                case "Эльф": { MaxHP = 2000; CurrentHP = 2000; ExpCharacter = 0; break; }
                case "Орк": { MaxHP = 1200; CurrentHP = 1200; ExpCharacter = 0; break; }
                case "Гоблин": { MaxHP = 800; CurrentHP = 800; ExpCharacter = 0; break; }
            }
            switch (GenderCharacter)
            {
                case "Мужчина": { MaxHP += 50; CurrentHP += 50; break; }
            }
            StateCharacter = "Здоров";
            CanMove = true;
            CanSpeak = true;
            IsParalyzedCharacter = false;
            IsPoisonedCharacter = false;
            IsPoisonedCharacter = false;
            StartStateTask();

        }
        //

        //help methods
        public void Compareing()
        {
            bool ok = false;
            int insert = default(int);
            while (!ok)
            {
                Console.WriteLine("Выберите персонажа с которым вы хотите сравнить персонажа {0}:", NameCharacter);
                int buf = 1;
                Console.WriteLine("0: Выход.");
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.StateCharacter != (string)StatesForCharacter[5]) { if (chr.UnicCharacterNumber != UnicCharacterNumber) { Console.WriteLine("{0}.{1} ", buf, chr.NameCharacter); buf++; } }
                }
                try
                {
                    insert = Convert.ToInt32(Console.ReadLine());
                    if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                    else if (insert == 0) { ok = true; }
                    else
                    {
                        RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                        buf = 1;
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.StateCharacter != (string)RolePlayCharacter.StatesForCharacter[5]&&chr.UnicCharacterNumber!=UnicCharacterNumber)
                            {
                                if (buf == insert)
                                {
                                    AimCharacter = chr;
                                    ok = true;
                                    CompareCharacter(AimCharacter);
                                    break;
                                }
                                else buf++;
                            }
                        }
                    }
                }
                catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
            }
        }
        protected void CompareCharacter(RolePlayCharacter chr)
        {
            if (CompareTo(chr) == 1) Console.WriteLine("Персонаж {0} превосходит персонажа {1} по опыту", NameCharacter, chr.NameCharacter);
            else if (CompareTo(chr) == 0) Console.WriteLine("Персонажи имеют одинаковое количество очков опыта");
            else Console.WriteLine("Персонаж {0} имеет меньше очков опыта чем у  персонажа {1}", NameCharacter, chr.NameCharacter);
        }
        public virtual void StartStateTask()
        {
            Task StatesCharacterTask = new Task(() =>
            {
                
                    if (CurrentHP > 0)
                        if (IsParalyzedCharacter) StateCharacter = (string)StatesForCharacter[4];
                        else if (IsSickCharacter) StateCharacter = (string)StatesForCharacter[2];
                        else if (isPoisonedCharacter) StateCharacter = (string)StatesForCharacter[3];
                        else
                        {
                            if (PercentsHP < 10.0)
                                StateCharacter = (string)StatesForCharacter[1];
                            else if (PercentsHP >= 10.0) StateCharacter = (string)StatesForCharacter[0];
                        }
                    else
                    {
                        StateCharacter = (string)StatesForCharacter[5];
                        IsParalyzedCharacter = false;
                        IsPoisonedCharacter = false;
                        IsSickCharacter = false;
                        CanMove = false;
                        CanSpeak = false;
                    }
                
            });
            StatesCharacterTask.Start();
        }
        protected static bool isNameCorrect(string name)
        {
            string undefinedSymbols = "\",!#$%^&*()=+\\./ ";
            if (String.IsNullOrEmpty(name)) return false;

            foreach (char c in name)
            {
                if (Char.IsNumber(c) || undefinedSymbols.Contains(c)) return false;
            }
            return true;
        }
        protected static string ChooseRace()
        {
            ConsoleKeyInfo key;
            while (true)
            {
                Console.WriteLine("Выберите расу:\n1.Человек\n2.Гном\n3.Эльф\n4.Орк\n5.Гоблин");
                key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1': { return "Человек"; }
                    case '2': { return "Гном"; }
                    case '3': { return "Эльф"; }
                    case '4': { return "Орк"; }
                    case '5': { return "Гоблин"; }
                }
            }
        }
        protected static string ChooseGender()
        {
            while (true)
            {
                Console.WriteLine();
                ConsoleKeyInfo key;
                Console.WriteLine("Выберите пол:\n1.Мужчина\n2.Женщина");
                key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1': { return "Мужчина"; }
                    case '2': { return "Женщина"; }
                }
            }
        }
        public int CompareTo(RolePlayCharacter obj)
        {

            if (ExpCharacter > obj.ExpCharacter) return 1;
            else
            if (ExpCharacter == obj.ExpCharacter) return 0;
            else return -1;
        }
        public override string ToString()
        {
            string canMoveString = default(string);
            string canSpeakString = default(string);
            string invulnerability = default(string);
            string poisoned = default(string);
            string sick = default(string);
            if (IsSickCharacter) sick = "Да";
            else sick = "Нет";
            //
            if (IsPoisonedCharacter) poisoned = "Да";
            else poisoned = "Нет";
            //
            if (CanMove) { canMoveString = "Да"; }
            else canMoveString = "Нет";
            //
            if (CanSpeak) { canSpeakString = "Да"; }
            else canSpeakString = "Нет";
            //
            if (IsInvulnerable) invulnerability = "Да";
            else invulnerability = "Нет";
            return "\nУникальный номер персонажа: " + UnicCharacterNumber + "\nИмя: " + NameCharacter + "\nРаса: " + RaceCharacter + "\nПол: " + GenderCharacter + "\nВозраст: " + AgeCharacter + "\nСостояние персонажа: " + StateCharacter + "\nТекущее количество здоровья: " + CurrentHP + "\nТекущее здоровье в процентах " + Math.Round(PercentsHP, 1) + "\nМаксимальное количество здоровья " + MaxHP + "\nТекущее количество опыта: " + ExpCharacter + "\nНеуязвим?: " + invulnerability + "\nОтравлен?:" + poisoned + "\nБолен?:" + sick + "\nМожет говорить?: " + canSpeakString + "\nМожет ходить?: " + canMoveString;
        }
        //
        //inventory
        protected ArrayList AvailableArtefacts = new ArrayList() { "Маленькая бутылка с живой водой", "Средняя бутылка с живой водой", "Большая бутылка с живой водой", "Маленькая бутылка с мертвой водой", "Средняя бутылка с мертвой водой", "Большая бутылка с мертвой водой", "Посох молнии", "Декокт из лягушачих лапок", "Ядовитая слюна", "Глаз василиска" };
        public void AddArtefactToInventory()
        {
            bool ok = false;
            while (!ok)
            {
                Console.WriteLine("Выберите артефакт, который вы хотите положить в инвентарь персонажа {0}:", NameCharacter);
                int buf = 1;
                int insert = default(int);
                Console.WriteLine("0: Выход.");
                foreach (string item in AvailableArtefacts)
                {
                    Console.WriteLine("{0}.{1}", buf, item); buf++;
                }
                try
                {
                    insert = Convert.ToInt32(Console.ReadLine());
                    if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                    else if (insert == 0) { ok = true; }
                    else
                    {
                        buf = 1;
                        foreach (string item in AvailableArtefacts)
                        {
                            if (buf == insert)
                            {
                                ok = true;
                                AddArtefact((string)AvailableArtefacts[buf - 1]);
                                Program.RefreshCastingAbilities();
                                break;
                            }
                            else buf++;
                        }
                    }
                }
                catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
            }
        }
        protected bool AddArtefact(string name)
        {
            if (name == "Маленькая бутылка с живой водой") Inventory.Add(new LittleBottleWithLiveWater());
            else if (name == "Средняя бутылка с живой водой") Inventory.Add(new MiddleBottleWithLiveWater());
            else if (name == "Большая бутылка с живой водой") Inventory.Add(new LargeBottleWithLiveWater());
            else if (name == "Маленькая бутылка с мертвой водой") Inventory.Add(new LittleBottleWithDeadWater());
            else if (name == "Средняя бутылка с мертвой водой") Inventory.Add(new MiddleBottleWithDeadWater());
            else if (name == "Большая бутылка с мертвой водой") Inventory.Add(new LargeBottleWithDeadWater());
            else if (name == "Декокт из лягушачих лапок") Inventory.Add(new FrogDeck());
            else if (name == "Глаз василиска") Inventory.Add(new BasiliskEye());
            else
            {
                if (name == "Посох молнии")
                {
                    bool exist = false;

                    foreach (Artefact item in Inventory)
                    {
                        if (item.Name == "Посох молнии") { Console.WriteLine("В инвентаре персонажа {0} уже есть данный артефакт. Так как он возобновлемый - персонажу не нужен ещё один такой артефакт",UnicCharacterNumber); exist = true; return false; }
                    }
                    if (!exist)
                        Inventory.Add(new StaffOfLightning());

                }
                else if (name == "Ядовитая слюна")
                {
                    bool exist = false;
                    foreach (Artefact item in Inventory)
                    {
                        if (item.Name == "Ядовитая слюна") { Console.WriteLine("В инвентаре персонажа {0} уже есть данный артефакт. Так как он возобновлемый - персонажу не нужен ещё один такой артефакт"); exist = true; return false; }
                    }
                    if (!exist)
                        Inventory.Add(new PoisonousSaliva());
                }
            }
            return true;

        }
        public void DeleteArtefactFromInventory()
        {
            bool ok = false;
            if (Inventory.Count != 0)
            {
                while (!ok)
                {
                    Console.WriteLine("Выберите артефакт, который вы хотите выбросить из инвентаря персонажа {0}:", NameCharacter);
                    int buf = 1;
                    int insert = default(int);
                    Console.WriteLine("0: Выход.");
                    foreach (Artefact item in Inventory)
                    {
                        Console.WriteLine("{0}.{1}", buf, item.Name); buf++;
                    }
                    try
                    {
                        insert = Convert.ToInt32(Console.ReadLine());
                        if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                        else if (insert == 0) { ok = true; }
                        else
                        {
                            buf = 1;
                            foreach (Artefact chr in Inventory)
                            {
                                if (buf == insert)
                                {
                                    ok = true;
                                    Inventory.RemoveAt(buf - 1);
                                    Console.WriteLine("Артефакт был выброшен.");
                                    Program.RefreshCastingAbilities();
                                    break;
                                }
                                else buf++;
                            }
                        }
                    }
                    catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                }
            }
            else Console.WriteLine("Инвентарь персонажа {0} пуст!", NameCharacter);
        }
        public void ShowInventory()
        {
            int buf = 1;
            if (Inventory.Count != 0)
            {
                Console.WriteLine("Инвентарь персонажа {0}", nameCharacter);
                foreach (Artefact item in Inventory)
                {
                    Console.WriteLine("{0}.{1}", buf, item.Name); buf++;
                }
            }
            else Console.WriteLine("Инентарь персонажа {0} пуст", nameCharacter);
        }
        public void GiveArtefact()
        {
            bool ok = false;
            if (Inventory.Count != 0)
            {
                while (!ok)
                {
                    Console.WriteLine("Выберите артефакт, который вы хотите передать:");
                    int buf = 1;
                    int insert = default(int);
                    Console.WriteLine("0: Выход.");
                    foreach (Artefact item in Inventory)
                    {
                        Console.WriteLine("{0}.{1}", buf, item.Name); buf++;
                    }
                    try
                    {
                        insert = Convert.ToInt32(Console.ReadLine());
                        if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. Попробуйте ещё раз."); continue; }
                        else if (insert == 0) { ok = true; }
                        else
                        {
                            buf = 1;
                            uint id = ChooseCharacterToGive(UnicCharacterNumber);
                            RolePlayCharacter aimChr = default(RolePlayCharacter);
                            foreach (RolePlayCharacter chr in Program.CharactersList)
                            {
                                if (chr.UnicCharacterNumber == id) aimChr = chr;
                            }
                            foreach (Artefact chr in Inventory)
                            {
                                Type type = default(Type);
                                if (buf == insert)
                                {
                                    type = chr.GetType();
                                    switch (type.Name)
                                    {
                                        case "LittleBottleWithLiveWater": { LittleBottleWithLiveWater obj = chr as LittleBottleWithLiveWater; if (aimChr.AddArtefact((string)AvailableArtefacts[0])) { Console.WriteLine("Артефакт был передан"); Inventory.RemoveAt(buf - 1); } else Console.WriteLine("У этого персонажа уже есть такой артефакт. Артефакт останеся у вас."); break; }
                                        case "MiddleBottleWithLiveWater": { MiddleBottleWithLiveWater obj = chr as MiddleBottleWithLiveWater; if (aimChr.AddArtefact((string)AvailableArtefacts[1])) { Console.WriteLine("Артефакт был передан"); Inventory.RemoveAt(buf - 1); } else Console.WriteLine("У этого персонажа уже есть такой артефакт. Артефакт останеся у вас."); break; }
                                        case "LargeBottleWithLiveWater": { LargeBottleWithLiveWater obj = chr as LargeBottleWithLiveWater; if (aimChr.AddArtefact((string)AvailableArtefacts[2])) { Console.WriteLine("Артефакт был передан"); Inventory.RemoveAt(buf - 1); } else Console.WriteLine("У этого персонажа уже есть такой артефакт. Артефакт останеся у вас."); break; }
                                        case "LittleBottleWithDeadWater": { LittleBottleWithDeadWater obj = chr as LittleBottleWithDeadWater; if (aimChr.AddArtefact((string)AvailableArtefacts[3])) { Console.WriteLine("Артефакт был передан"); Inventory.RemoveAt(buf - 1); } else Console.WriteLine("У этого персонажа уже есть такой артефакт. Артефакт останеся у вас."); break; }
                                        case "MiddleBottleWithDeadWater": { MiddleBottleWithDeadWater obj = chr as MiddleBottleWithDeadWater; if (aimChr.AddArtefact((string)AvailableArtefacts[4])) { Console.WriteLine("Артефакт был передан"); Inventory.RemoveAt(buf - 1); } else Console.WriteLine("У этого персонажа уже есть такой артефакт. Артефакт останеся у вас."); break; }
                                        case "LargeBottleWithDeadWater": { LargeBottleWithDeadWater obj = chr as LargeBottleWithDeadWater; if (aimChr.AddArtefact((string)AvailableArtefacts[5])) { Console.WriteLine("Артефакт был передан"); Inventory.RemoveAt(buf - 1); } else Console.WriteLine("У этого персонажа уже есть такой артефакт. Артефакт останеся у вас."); break; }
                                        case "StaffOfLightning": { StaffOfLightning obj = chr as StaffOfLightning; if (aimChr.AddArtefact((string)AvailableArtefacts[6])) { Console.WriteLine("Артефакт был передан"); Inventory.RemoveAt(buf - 1); } else Console.WriteLine("У этого персонажа уже есть такой артефакт. Артефакт останеся у вас."); break; }
                                        case "PoisonousSaliva": { PoisonousSaliva obj = chr as PoisonousSaliva; if (aimChr.AddArtefact((string)AvailableArtefacts[8])) { Console.WriteLine("Артефакт был передан"); Inventory.RemoveAt(buf - 1); } else Console.WriteLine("У этого персонажа уже есть такой артефакт. Артефакт останеся у вас."); break; }
                                        case "FrogDeck": { FrogDeck obj = chr as FrogDeck; obj.ArtefactCast(unicCharacterNumber); if (aimChr.AddArtefact((string)AvailableArtefacts[7])) { Console.WriteLine("Артефакт был передан"); Inventory.RemoveAt(buf - 1); } else Console.WriteLine("У этого персонажа уже есть такой артефакт. Артефакт останеся у вас."); break; }
                                        case "BasiliskEye": { BasiliskEye obj = chr as BasiliskEye; if (aimChr.AddArtefact((string)AvailableArtefacts[9])) { Console.WriteLine("Артефакт был передан"); Inventory.RemoveAt(buf - 1); } else Console.WriteLine("У этого персонажа уже есть такой артефакт. Артефакт останеся у вас."); break; }
                                        default:
                                            { Console.WriteLine("Нет такого артефакта"); }
                                            break;
                                    }
                                    ok = true;
                                    Program.RefreshCastingAbilities();
                                    break;
                                }
                                else buf++;
                            }
                        }
                    }
                    catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                }
            }
            else Console.WriteLine("Инвентарь персонажа {0} пуст!", NameCharacter);
        }
        protected uint ChooseCharacterToGive(uint currentID)
        {
            bool ok = false;
            int insert = default(int);
            RolePlayCharacter CurrentChr = default(RolePlayCharacter);
            foreach (RolePlayCharacter item in Program.CharactersList)
            {
                if (item.UnicCharacterNumber == currentID) CurrentChr = item;
            }
            while (!ok)
            {
                Console.WriteLine("Выберите персонажа которому {0} должен отдать артефакт:", CurrentChr.NameCharacter);
                int buf = 1;
                Console.WriteLine("0: Выход.");
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.StateCharacter != "Мертв" && chr.UnicCharacterNumber != CurrentChr.UnicCharacterNumber) { Console.WriteLine("{0}.{1} ", buf, chr.NameCharacter); buf++; }
                }
                try
                {
                    insert = Convert.ToInt32(Console.ReadLine());
                    if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                    else if (insert == 0) { ok = true; }
                    else
                    {
                        buf = 1;
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.StateCharacter != "Мертв" && chr.UnicCharacterNumber != CurrentChr.UnicCharacterNumber)
                            {
                                if (buf == insert)
                                {
                                    return chr.UnicCharacterNumber;
                                }
                                else buf++;
                            }


                        }
                    }
                }
                catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
            }
            return 0;
        }
        public void UseArtefact()
        {
            bool ok = false;
            if (Inventory.Count != 0)
            {
                while (!ok)
                {
                    Console.WriteLine("Выберите артефакт, который вы хотите использовать:");
                    int buf = 1;
                    int insert = default(int);
                    Console.WriteLine("0: Выход.");
                    foreach (Artefact item in Inventory)
                    {
                        Console.WriteLine("{0}.{1}", buf, item.Name); buf++;
                    }
                    try
                    {
                        insert = Convert.ToInt32(Console.ReadLine());
                        if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. Попробуйте ещё раз."); continue; }
                        else if (insert == 0) { ok = true; }
                        else
                        {
                            buf = 1;
                            foreach (Artefact chr in Inventory)
                            {
                                Type type = default(Type);
                                if (buf == insert)
                                {
                                    type = chr.GetType();
                                    switch (type.Name)
                                    {
                                        case "LittleBottleWithLiveWater": { LittleBottleWithLiveWater obj = chr as LittleBottleWithLiveWater; obj.ArtefactCast(unicCharacterNumber); if (obj.ReChargeable == false && obj.CanUse == false) { Inventory.RemoveAt(buf - 1); Console.WriteLine("Артефакт был выброшен"); } break; }
                                        case "MiddleBottleWithLiveWater": { MiddleBottleWithLiveWater obj = chr as MiddleBottleWithLiveWater; obj.ArtefactCast(unicCharacterNumber); if (obj.ReChargeable == false && obj.CanUse == false) { Inventory.RemoveAt(buf - 1); Console.WriteLine("Артефакт был выброшен"); } break; }
                                        case "LargeBottleWithLiveWater": { LargeBottleWithLiveWater obj = chr as LargeBottleWithLiveWater; obj.ArtefactCast(unicCharacterNumber); if (obj.ReChargeable == false && obj.CanUse == false) { Inventory.RemoveAt(buf - 1); Console.WriteLine("Артефакт был выброшен"); } break; }
                                        case "LittleBottleWithDeadWater": { LittleBottleWithDeadWater obj = chr as LittleBottleWithDeadWater; obj.ArtefactCast(unicCharacterNumber); if (obj.ReChargeable == false && obj.CanUse == false) { Inventory.RemoveAt(buf - 1); Console.WriteLine("Артефакт был выброшен"); } break; }
                                        case "MiddleBottleWithDeadWater": { MiddleBottleWithDeadWater obj = chr as MiddleBottleWithDeadWater; obj.ArtefactCast(unicCharacterNumber); if (obj.ReChargeable == false && obj.CanUse == false) { Inventory.RemoveAt(buf - 1); Console.WriteLine("Артефакт был выброшен"); } break; }
                                        case "LargeBottleWithDeadWater": { LargeBottleWithDeadWater obj = chr as LargeBottleWithDeadWater; obj.ArtefactCast(unicCharacterNumber); if (obj.ReChargeable == false && obj.CanUse == false) { Inventory.RemoveAt(buf - 1); Console.WriteLine("Артефакт был выброшен"); } break; }
                                        case "StaffOfLightning":
                                            {
                                                Type currentType = default(Type);
                                                currentType = GetType();
                                                if (currentType.Name == "MagicRolePlayCharacter")
                                                {
                                                    StaffOfLightning obj = chr as StaffOfLightning;
                                                    obj.ArtefactCast(UnicCharacterNumber);
                                                }
                                                else Console.WriteLine("Персонаж {0} не моет использовать этот предмет, так как не владеет магией.", nameCharacter);
                                                break;
                                            }
                                        case "PoisonousSaliva":
                                            {
                                                Type currentType = default(Type);
                                                currentType = GetType();
                                                if (currentType.Name == "MagicRolePlayCharacter")
                                                {
                                                    PoisonousSaliva obj = chr as PoisonousSaliva;
                                                    obj.ArtefactCast(UnicCharacterNumber);
                                                }
                                                else Console.WriteLine("Персонаж {0} не моет использовать этот предмет, так как не владеет магией.", nameCharacter);
                                                break;
                                            }
                                        case "FrogDeck": { FrogDeck obj = chr as FrogDeck; obj.ArtefactCast(unicCharacterNumber); if (obj.ReChargeable == false && obj.CanUse == false) { Inventory.RemoveAt(buf - 1); Console.WriteLine("Артефакт был выброшен"); } break; }
                                        case "BasiliskEye": { BasiliskEye obj = chr as BasiliskEye; obj.ArtefactCast(unicCharacterNumber); if (obj.ReChargeable == false && obj.CanUse == false) { Inventory.RemoveAt(buf - 1); Console.WriteLine("Артефакт был выброшен"); } break; }
                                        default:
                                            { Console.WriteLine("Нет такого артефакта"); }
                                            break;
                                    }
                                    ok = true;
                                    Program.RefreshCastingAbilities();
                                    break;
                                }
                                else buf++;
                            }
                        }
                    }
                    catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                }
            }
            else Console.WriteLine("Инвентарь персонажа {0} пуст!", NameCharacter);
        }
        //
        public void SpeakWithCharacter()
        {
            ChooseDelegate choose = UnicCharacterNumber => {
                bool ok = false;
                int insert = default(int);
                RolePlayCharacter CurrentChr = default(RolePlayCharacter);
                foreach (RolePlayCharacter item in Program.CharactersList)
                {
                    if (item.UnicCharacterNumber == UnicCharacterNumber) CurrentChr = item;
                }
                while (!ok)
                {
                    Console.WriteLine("Выберите персонажа с которым персонаж {0} хочет поговорить:", CurrentChr.NameCharacter);
                    int buf = 1;
                    Console.WriteLine("0: Выход.");
                    foreach (RolePlayCharacter chr in Program.CharactersList)
                    {
                        if (chr.StateCharacter != "Мертв" && chr.UnicCharacterNumber != CurrentChr.UnicCharacterNumber) { Console.WriteLine("{0}.{1} ", buf, chr.NameCharacter); buf++; }
                    }
                    try
                    {
                        insert = Convert.ToInt32(Console.ReadLine());
                        if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                        else if (insert == 0) { ok = true; }
                        else
                        {
                            buf = 1;
                            foreach (RolePlayCharacter chr in Program.CharactersList)
                            {
                                if (chr.StateCharacter != "Мертв" && chr.UnicCharacterNumber != CurrentChr.UnicCharacterNumber)
                                {
                                    if (buf == insert)
                                    {
                                        return chr.UnicCharacterNumber;
                                    }
                                    else buf++;
                                }


                            }
                        }
                    }
                    catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                }
                return 0;
            };
            uint aimedId = choose(UnicCharacterNumber);
            RolePlayCharacter aimCharacter = default(RolePlayCharacter);
            foreach (RolePlayCharacter item in Program.CharactersList)
            {
                if(item.UnicCharacterNumber==aimedId) { aimCharacter = item; break; }
            }
            if (CanSpeak)
            {
                if (aimCharacter.CanSpeak)
                {
                    Console.WriteLine("Персонажи смогли поговорить");
                }
                else { Console.WriteLine("Сейчас персонаж {0} не может говорить", aimCharacter.NameCharacter); }
            }
            else Console.WriteLine("Выбранный вами персонаж не может говорить в данный момент.");
        }
        
        public void FightWithCharacter()
        {
            ChooseDelegate choose = UnicCharacterNumber => {
                bool ok = false;
                int insert = default(int);
                RolePlayCharacter CurrentChr = default(RolePlayCharacter);
                foreach (RolePlayCharacter item in Program.CharactersList)
                {
                    if (item.UnicCharacterNumber == UnicCharacterNumber) CurrentChr = item;
                }
                while (!ok)
                {
                    Console.WriteLine("Выберите персонажа с которым персонаж {0} хочет сразится:", CurrentChr.NameCharacter);
                    int buf = 1;
                    Console.WriteLine("0: Выход.");
                    foreach (RolePlayCharacter chr in Program.CharactersList)
                    {
                        if (chr.StateCharacter != "Мертв" && chr.IsParalyzedCharacter==false && chr.UnicCharacterNumber != CurrentChr.UnicCharacterNumber) { Console.WriteLine("{0}.{1} ", buf, chr.NameCharacter); buf++; }
                    }
                    try
                    {
                        insert = Convert.ToInt32(Console.ReadLine());
                        if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                        else if (insert == 0) { ok = true; }
                        else
                        {
                            buf = 1;
                            foreach (RolePlayCharacter chr in Program.CharactersList)
                            {
                                if (chr.StateCharacter != "Мертв" && chr.IsParalyzedCharacter == false && chr.UnicCharacterNumber != CurrentChr.UnicCharacterNumber)
                                {
                                    if (buf == insert)
                                    {
                                        return chr.UnicCharacterNumber;
                                    }
                                    else buf++;
                                }


                            }
                        }
                    }
                    catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                }
                return 0;
            };
            uint aimedId = choose(UnicCharacterNumber);
            if (aimedId == 0) return;
            RolePlayCharacter aimChr = default(RolePlayCharacter);
            foreach (RolePlayCharacter item in Program.CharactersList)
            {
               
                if(item.UnicCharacterNumber==aimedId) { aimChr = item; break; }
            }
            uint chance1 = 50;
            uint chance2 = 50;
            if (ExpCharacter > aimChr.ExpCharacter) { chance1 += 10; chance2 -= 10; }
            else if(ExpCharacter < aimChr.ExpCharacter) { chance1 -= 10; chance2 += 10; }
            string classCurrent = default(string);
            string classAim = default(string);

            foreach (object item in Program.CharactersList)
            {
                Type type = default(Type);
                type = item.GetType();
                if (type.Name == "RolePlayCharacter")
                {
                    RolePlayCharacter curr = default(RolePlayCharacter);
                    curr = item as RolePlayCharacter;
                    if (curr.UnicCharacterNumber == UnicCharacterNumber) { classCurrent = type.Name;  }
                    else if (curr.UnicCharacterNumber == aimChr.UnicCharacterNumber) { classAim = type.Name; }
                }
                else
                {
                    MagicRolePlayCharacter curr = default(MagicRolePlayCharacter);
                    curr = item as MagicRolePlayCharacter;
                    if (curr.UnicCharacterNumber == UnicCharacterNumber) { classCurrent = "MagicRolePlayCharacter"; }
                    else if (curr.UnicCharacterNumber == aimChr.UnicCharacterNumber) { classAim = "MagicRolePlayCharacter"; }
                }
            }
            if (classCurrent != classAim)
            {
                if (classCurrent == "RolePlayCharacter" && classAim == "MagicRolePlayCharacter") { chance1 -= 10; chance2 += 10; }
                else if (classCurrent == "MagicRolePlayCharacter" && classAim == "RolePlayCharacter") { chance1 += 10; chance2 -= 10;}
            }
            if (CurrentHP > aimChr.CurrentHP) { chance1 += 10; chance2 -= 10; }
            else if(aimChr.CurrentHP<CurrentHP) { chance1 -= 10; chance2 += 10; }
            int[] rand = new int[100];
            for (int i = 0; i < chance1; i++)
                rand[i] = 1;
            for (uint i = chance1; i < (uint)rand.Length; i++)
                rand[i] = 2;
            bool can_run = default(bool);
            Random random = new Random();
            int rand_number = random.Next(1, 99);
            if (rand_number%2==0) can_run = false;
            else can_run = true;
            if (!IsInvulnerable && !aimChr.IsInvulnerable)
            {
                if (rand[random.Next(0, 99)] == 1)
                {
                    if (can_run)
                    {

                        Console.WriteLine("Персонаж {0} победил в сражении, потеряв {3} очков здоровья и получил {2} опыта, но {1} смог сбежать, и потерял {2} единиц здоровья.", NameCharacter, aimChr.NameCharacter, aimChr.CurrentHP / 2,aimChr.CurrentHP/4);
                        ExpCharacter += aimChr.CurrentHP / 2;
                        CurrentHP -= aimChr.CurrentHP / 4;
                        aimChr.CurrentHP -= aimChr.CurrentHP / 2;
                    }
                    else
                    {

                        Console.WriteLine("Персонаж {0} победил в сражении и получил {2} опыта, но потерял {3} очков здоровья. Персонаж {1} умирает.", NameCharacter, aimChr.NameCharacter, aimChr.CurrentHP, CurrentHP / 4);
                        ExpCharacter += aimChr.CurrentHP;
                        CurrentHP -= CurrentHP / 4;
                        aimChr.CurrentHP = 0;
                    }
                }
                else
                {
                    if (can_run)
                    {

                        Console.WriteLine("Персонаж {0} победил в сражении, потеряв {3} очков здоровья и получил {2} опыта, но {1} смог сбежать, и потерял {2} единиц здоровья.", aimChr.NameCharacter, NameCharacter, CurrentHP / 2,CurrentHP/4);
                        aimChr.ExpCharacter += CurrentHP / 2;
                        aimChr.CurrentHP -= CurrentHP / 4;
                        CurrentHP -= CurrentHP / 2;
                    }
                    else
                    {

                        Console.WriteLine("Персонаж {0} победил в сражении и получил {2} опыта, но потерял {3} очков здоровья. Персонаж {1} умирает.", aimChr.NameCharacter, NameCharacter, CurrentHP, aimChr.CurrentHP / 4);
                        aimChr.ExpCharacter += CurrentHP;
                        aimChr.CurrentHP -= aimChr.CurrentHP / 4;
                        CurrentHP = 0;
                    }
                }
            }
            else
            {
                if (IsInvulnerable && aimChr.IsInvulnerable) { Console.WriteLine("Персонажи неуязвимы в данный момент, поэтому они не смогли убить друг друга"); }
                else if(IsInvulnerable && !aimChr.IsInvulnerable)
                {
                    Console.WriteLine("Персонаж {0} был неуязвим и победил в сражении, получив {2} опыта. Персонаж {1} умирает.", NameCharacter, aimChr.NameCharacter, aimChr.CurrentHP);
                    ExpCharacter += aimChr.CurrentHP;
                    aimChr.CurrentHP = 0;
                }
                else if(!IsInvulnerable && aimChr.IsInvulnerable)
                {
                    Console.WriteLine("Персонаж {0} был неуязвим и победил в сражении, получив {2} опыта. Персонаж {1} умирает.", aimChr.NameCharacter, NameCharacter, CurrentHP);
                    aimChr.ExpCharacter += CurrentHP;
                    CurrentHP = 0;
                }
            }
            Program.RefreshInfo();
        }
        //
    }
    class MagicRolePlayCharacter : RolePlayCharacter
        {
            //merges
            private uint currentMana;
            private uint maxMana;
            private bool isCastsMagic;
            //
            //properties
            public uint MaxMana { get => maxMana; private set => maxMana = value; }
            public uint CurrentMana { get => currentMana; set => currentMana = value; }
            public bool IsCastsMagic { get => isCastsMagic; set => isCastsMagic = value; }
            //
            //
            public MagicRolePlayCharacter() : base()
            {
                switch (RaceCharacter)
                {
                    case "Гоблин": { MaxMana = 200; CurrentMana = 200; break; }
                    case "Орк": { MaxMana = 450; CurrentMana = 450; break; }
                    case "Гном": { MaxMana = 550; CurrentMana = 550; break; }
                    case "Человек": { MaxMana = 750; CurrentMana = 750; break; }
                    case "Эльф": { MaxMana = 1000; CurrentMana = 1000; break; }
                }
                StartStateTask();

            }

            public override void StartStateTask()
            {
                Task StatesCharacterTask = new Task(() =>
                {

                    if (StateCharacter == (string)StatesForCharacter[5]) { IsCastsMagic = false; CurrentMana = 0; }
                    else if (IsCastsMagic == false) { CanMove = true; CanSpeak = true; }

                        if (CurrentHP > 0)
                            if (IsParalyzedCharacter) StateCharacter = (string)StatesForCharacter[4];
                            else if (IsSickCharacter) StateCharacter = (string)StatesForCharacter[2];
                            else if (isPoisonedCharacter) StateCharacter = (string)StatesForCharacter[3];
                            else
                            {
                                if (PercentsHP < 10.0)
                                    StateCharacter = (string)StatesForCharacter[1];
                                else if (PercentsHP >= 10.0) StateCharacter = (string)StatesForCharacter[0];
                            }
                        else
                        {
                            StateCharacter = (string)StatesForCharacter[5];
                            IsParalyzedCharacter = false;
                            IsPoisonedCharacter = false;
                            IsSickCharacter = false;
                            CanMove = false;
                            CanSpeak = false;
                        }
                    
                });
                StatesCharacterTask.Start();
            }
            public override string ToString()
            {
                string canMoveString = default(string);
                string canSpeakString = default(string);
                string invulnerability = default(string);
                string poisoned = default(string);
                string sick = default(string);
                if (IsSickCharacter) sick = "Да";
                else sick = "Нет";
                //
                if (IsPoisonedCharacter) poisoned = "Да";
                else poisoned = "Нет";
                //
                if (CanMove) { canMoveString = "Да"; }
                else canMoveString = "Нет";
                //
                if (CanSpeak) { canSpeakString = "Да"; }
                else canSpeakString = "Нет";
                if (IsInvulnerable) invulnerability = "Да";
                else invulnerability = "Нет";
                //

                return "\nУникальный номер персонажа: " + UnicCharacterNumber + "\nИмя: " + NameCharacter + "\nРаса: " + RaceCharacter + "\nПол: " + GenderCharacter + "\nВозраст: " + AgeCharacter + "\nСостояние персонажа: " + StateCharacter + "\nТекущее количество здоровья: " + CurrentHP + "\nТекущее здоровье в процентах: " + Math.Round(PercentsHP, 1) + "\nМаксимальное количество здоровья: " + MaxHP + "\nТекущее количество маны: " + CurrentMana + "\nМаксимальное количество маны: " + MaxMana + "\nТекущее количество опыта: " + ExpCharacter + "\nНеуязвим?: " + invulnerability + "\nОтравлен?:" + poisoned + "\nБолен?:" + sick + "\nМожет говорить?: " + canSpeakString + "\nМожет ходить?: " + canMoveString;
            }
        //magic
        ArrayList SpellBook = new ArrayList();
        public ArrayList AvailableSpells = new ArrayList() { "Добавить здоровье", "Вылечить", "Противоядие", "Оживить", "Броня", "Отомри" };
        public void LearnSpell()
        {
            bool ok = false;
            while (!ok)
            {
                Console.WriteLine("Выберите заклинание, которое персонаж {0} хочет выучить:", NameCharacter);
                int buf = 1;
                int insert = default(int);
                Console.WriteLine("0: Выход.");
                foreach (string item in AvailableSpells)
                {
                    Console.WriteLine("{0}.{1}", buf, item); buf++;
                }
                try
                {
                    insert = Convert.ToInt32(Console.ReadLine());
                    if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                    else if (insert == 0) { ok = true; }
                    else
                    {
                        buf = 1;
                        foreach (string item in AvailableSpells)
                        {
                            if (buf == insert)
                            {
                                ok = true;
                                AddSpellToSpellBook((string)AvailableSpells[buf - 1]);
                                break;
                            }
                            else buf++;
                        }
                    }
                }
                catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
            }
        }
        void AddSpellToSpellBook(string name)
        {
         
                if (name == "Добавить здоровье")
                {
                    bool exist = false;

                    foreach (Spell item in SpellBook)
                    {
                        if (item.Name == "Добавить здоровье") { Console.WriteLine("Персонаж {0} уже изучил это заклнание. ", NameCharacter); exist = true;}
                    }
                    if (!exist)
                        SpellBook.Add(new AddHealth());
                }
            else if (name == "Вылечить")
            {
                bool exist = false;

                foreach (Spell item in SpellBook)
                {
                    if (item.Name == "Вылечить") { Console.WriteLine("Персонаж {0} уже изучил это заклнание. ", NameCharacter); exist = true; }
                }
                if (!exist)
                    SpellBook.Add(new Heal());
            }
            else if (name == "Противоядие")
            {
                bool exist = false;

                foreach (Spell item in SpellBook)
                {
                    if (item.Name == "Противоядие") { Console.WriteLine("Персонаж {0} уже изучил это заклнание. ", NameCharacter); exist = true; }
                }
                if (!exist)
                    SpellBook.Add(new AntiDote());
            }
            else if (name == "Оживить")
            {
                bool exist = false;

                foreach (Spell item in SpellBook)
                {
                    if (item.Name == "Оживить") { Console.WriteLine("Персонаж {0} уже изучил это заклнание. ", NameCharacter); exist = true; }
                }
                if (!exist)
                    SpellBook.Add(new ReAnimate());
            }
            else if (name == "Броня")
            {
                bool exist = false;

                foreach (Spell item in SpellBook)
                {
                    if (item.Name == "Броня") { Console.WriteLine("Персонаж {0} уже изучил это заклнание. ", NameCharacter); exist = true; }
                }
                if (!exist)
                    SpellBook.Add(new Armor());
            }
            else if (name == "Отомри")
            {
                bool exist = false;

                foreach (Spell item in SpellBook)
                {
                    if (item.Name == "Отомри") { Console.WriteLine("Персонаж {0} уже изучил это заклнание. ", NameCharacter); exist = true; }
                }
                if (!exist)
                    SpellBook.Add(new TakeOffParalyzed());
            }
        }
        public void ForgetSpell()
        {
            bool ok = false;
            if (SpellBook.Count != 0)
            {
                while (!ok)
                {
                    Console.WriteLine("Выберите заклинание, которое персонаж {0} должен забыть:", NameCharacter);
                    int buf = 1;
                    int insert = default(int);
                    Console.WriteLine("0: Выход.");
                    foreach (Spell item in SpellBook)
                    {
                        Console.WriteLine("{0}.{1}", buf, item.Name); buf++;
                    }
                    try
                    {
                        insert = Convert.ToInt32(Console.ReadLine());
                        if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                        else if (insert == 0) { ok = true; }
                        else
                        {
                            buf = 1;
                            foreach (Spell chr in SpellBook)
                            {
                                if (buf == insert)
                                {
                                    ok = true;
                                    SpellBook.RemoveAt(buf - 1);
                                    Console.WriteLine("Заклинание было забыто.");
                                    break;
                                }
                                else buf++;
                            }
                        }
                    }
                    catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                }
            }
            else Console.WriteLine("Персонаж {0} не знает ни одного заклинания", NameCharacter);
        }
        public void ShowSpellBook()
        {
            Console.WriteLine("Заклинания, которые изучил перснаж {0}:", NameCharacter);
            int buf = 1;
            foreach (Spell item in SpellBook)
            {
                Console.WriteLine("{0}.{1}", buf, item.Name); buf++;
            }
        }
        public void UseSpell()
        {
            bool ok = false;
            if (SpellBook.Count != 0)
            {
                while (!ok)
                {
                    Console.WriteLine("Выберите заклинание, которое вы хотите использовать:");
                    int buf = 1;
                    int insert = default(int);
                    Console.WriteLine("0: Выход.");
                    foreach (Spell item in SpellBook)
                    {
                        Console.WriteLine("{0}.{1}", buf, item.Name); buf++;
                    }
                    try
                    {
                        insert = Convert.ToInt32(Console.ReadLine());
                        if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. Попробуйте ещё раз."); continue; }
                        else if (insert == 0) { ok = true; }
                        else
                        {
                            buf = 1;
                            foreach (Spell chr in SpellBook)
                            {
                                Type type = default(Type);
                                if (buf == insert)
                                {
                                    type = chr.GetType();
                                    switch (type.Name)
                                    {
                                        case "AddHealth": { AddHealth obj = chr as AddHealth; obj.SpellCast(UnicCharacterNumber); break; }
                                        case "Heal": { Heal obj = chr as Heal; obj.SpellCast(UnicCharacterNumber); break; }
                                        case "AntiDote": { AntiDote obj = chr as AntiDote; obj.SpellCast(UnicCharacterNumber); break; }
                                        case "ReAnimate": { ReAnimate obj = chr as ReAnimate; obj.SpellCast(UnicCharacterNumber); break; }
                                        case "TakeOffParalyzed": { TakeOffParalyzed obj = chr as TakeOffParalyzed; obj.SpellCast(UnicCharacterNumber); break; }
                                        case "Armor": { Armor obj = chr as Armor; obj.SpellCast(UnicCharacterNumber); break; }
                                        default:
                                            { Console.WriteLine("Нет такого заклинания"); }
                                            break;
                                    }
                                    ok = true;
                                    break;
                                }
                                else buf++;
                            }
                        }
                    }
                    catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                }
            }
            else Console.WriteLine("Персонаж {0} не знает ни одного заклинания", NameCharacter);
        }
        //
        }
    //spells
        abstract class Spell : IMagic
        {
            protected uint minManaCost;
            protected  string name;
            protected bool bShouldSpeakTillCast;
            protected bool bShouldDoActionsTillCast;
            public uint MinManaCost { get => minManaCost; protected set => minManaCost = value; }
            public bool BShouldSpeakTillCast { get => bShouldSpeakTillCast; protected set => bShouldSpeakTillCast = value; }
            public bool BShouldDoActionsTillCast { get => bShouldDoActionsTillCast; protected set => bShouldDoActionsTillCast = value; }
            public string Name { get => name; protected set => name = value; }

        abstract public void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength);
        }
        class AddHealth : Spell
        {
            public AddHealth()
            {
                MinManaCost = 2;
                Name = "Добавить здоровье";
                BShouldDoActionsTillCast = false;
                BShouldSpeakTillCast = true;
            }
            public void SpellCast(uint ourChrId)
            {
                MagicRolePlayCharacter CurrentChr = default(MagicRolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr as MagicRolePlayCharacter; break; }
                if (CurrentChr.CurrentMana >= MinManaCost)
                {

                    bool ok = false;
                    int insert = default(int);
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить заклинание \"Добавить здоровье\":", CurrentChr.NameCharacter);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.StateCharacter != "Мертв" && chr.CurrentHP != chr.MaxHP) { if (chr.UnicCharacterNumber == CurrentChr.UnicCharacterNumber) Console.WriteLine("{0}.На себя <{1} очков здоровья из {2}>", buf, CurrentChr.CurrentHP, CurrentChr.MaxHP); else Console.WriteLine("{0}.{1} <{2} очков здоровья из {3}>", buf, chr.NameCharacter, chr.CurrentHP, chr.MaxHP); buf++; }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    if (chr.StateCharacter != "Мертв" && chr.CurrentHP != chr.MaxHP)
                                    {
                                        if (buf == insert)
                                        {
                                            AimCharacter = chr;
                                            ok = true;
                                            DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                            break;
                                        }
                                        else buf++;
                                    }
                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("У персонажа не хватает маны на использование этого заклинания с минимальной силой. (требуется минимум 2 маны)");
            }

            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                MagicRolePlayCharacter currentCharacter = null;
                RolePlayCharacter aimCharacter = null;
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == currentChrNumber) { currentCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == AimedChrNumber) { aimCharacter = chr; }
                }
                bool ok = false;
                uint maxHPAccordingToMana = currentCharacter.CurrentMana / 2;
                uint maxHPAccordingToAimHP = aimCharacter.MaxHP - aimCharacter.CurrentHP;
                uint MaxHPToADD = Math.Min(maxHPAccordingToAimHP, maxHPAccordingToMana);


                uint hp = default(uint);
                while (!ok)
                {
                    Console.WriteLine("У этого персонажа {0} единиц здоровья из {1}", aimCharacter.CurrentHP, aimCharacter.MaxHP);
                    try
                    {
                        Console.WriteLine("Введите число очков здоровья, которое вы хотите добавить персонажу (Максимум = {0}):", MaxHPToADD);
                        hp = Convert.ToUInt32(Console.ReadLine());
                        if (hp > MaxHPToADD) { Console.WriteLine("Вы ввели число, большее максимальнго. Попробуйте ещё раз."); continue; }
                        else
                        {
                            ok = true;
                            currentCharacter.CurrentMana -= hp * MinManaCost;
                            aimCharacter.CurrentHP += hp;
                            currentCharacter.IsCastsMagic = true;
                            currentCharacter.CanMove = !BShouldDoActionsTillCast;
                            currentCharacter.CanSpeak = !BShouldSpeakTillCast;
                        currentCharacter.ExpCharacter += hp / 2;
                            Console.WriteLine("Персонаж {0} потратил {1} маны и получил {4} очков опыта Текущее количество маны: {2} из {3}", currentCharacter.NameCharacter, hp * 2, currentCharacter.CurrentMana, currentCharacter.MaxMana,hp/2);
                            Console.WriteLine("Персонажу {0} было восстановлено {1} очков здоровья. Текущее здоровье: {2} из {3}", aimCharacter.NameCharacter, hp, aimCharacter.CurrentHP, aimCharacter.MaxHP);

                        Program.RefreshInfo();
                        }
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Число слишком большое,или меньше нуля. Попробуйте ещё раз");
                        continue;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Число введено некорректно. попробуйте ещё раз.");
                        continue;
                    }
                }

            }
        }
        class Heal : Spell
        {
            public Heal()
            {
                MinManaCost = 20;
                Name = "Вылечить";
                BShouldDoActionsTillCast = true;
                BShouldSpeakTillCast = false;
            }
            public void SpellCast(uint ourChrId)
            {
                MagicRolePlayCharacter CurrentChr = default(MagicRolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr as MagicRolePlayCharacter; break; }
                if (CurrentChr.CurrentMana >= MinManaCost)
                {

                    bool ok = false;
                    int insert = default(int);
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить заклинание \"Вылечить\":", CurrentChr.NameCharacter);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.IsSickCharacter) { if (chr.UnicCharacterNumber == CurrentChr.UnicCharacterNumber) Console.WriteLine("{0}.На себя <{1}>", buf, CurrentChr.StateCharacter); else Console.WriteLine("{0}.{1} <{2}>", buf, chr.NameCharacter, chr.StateCharacter); buf++; }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    if (chr.IsSickCharacter)
                                    {
                                        if (buf == insert)
                                        {
                                            AimCharacter = chr;
                                            ok = true;
                                            DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
      
                                            break;
                                        }
                                        else buf++;
                                    }
                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("У персонажа не хватает маны на использование этого заклинания. (требуется 20 маны)");
            }
            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                MagicRolePlayCharacter currentCharacter = default(MagicRolePlayCharacter);
                RolePlayCharacter aimCharacter = default(RolePlayCharacter);
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == currentChrNumber) { currentCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == AimedChrNumber) { aimCharacter = chr; }
                }
                aimCharacter.IsSickCharacter = false;
                currentCharacter.IsCastsMagic = true;
                currentCharacter.CanMove = !bShouldDoActionsTillCast;
                currentCharacter.CanSpeak = !BShouldSpeakTillCast;
                currentCharacter.CurrentMana -= MinManaCost;
            currentCharacter.ExpCharacter += 20;
                Console.WriteLine("Персонаж {0} потратил {1} маны и получил {4} очков опыта. Текущее количество маны: {2} из {3}", currentCharacter.NameCharacter, MinManaCost, currentCharacter.CurrentMana, currentCharacter.MaxMana, 20);
                Console.WriteLine("Персонаж {0} был вылечен.", aimCharacter.NameCharacter);
            }
        }
        class AntiDote : Spell
        {
            public AntiDote()
            {
                MinManaCost = 30;
                 Name = "Противоядие";
                 BShouldDoActionsTillCast = true;
                BShouldSpeakTillCast = false;
            }
            public void SpellCast(uint ourChrId)
            {
                MagicRolePlayCharacter CurrentChr = default(MagicRolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr as MagicRolePlayCharacter; break; }
                if (CurrentChr.CurrentMana >= MinManaCost)
                {

                    bool ok = false;
                    int insert = default(int);
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить заклинание \"Противоядие\":", CurrentChr.NameCharacter);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.IsPoisonedCharacter) { if (chr.UnicCharacterNumber == CurrentChr.UnicCharacterNumber) Console.WriteLine("{0}.На себя <{1}>", buf, CurrentChr.StateCharacter); else Console.WriteLine("{0}.{1} <{2}>", buf, chr.NameCharacter, chr.StateCharacter); buf++; }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    if (chr.IsPoisonedCharacter)
                                    {
                                        if (buf == insert)
                                        {
                                            AimCharacter = chr;
                                            ok = true;
                                            DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                            break;
                                        }
                                        else buf++;
                                    }
                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("У персонажа не хватает маны на использование этого заклинания. (требуется 30 маны)");
            }
            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                MagicRolePlayCharacter currentCharacter = default(MagicRolePlayCharacter);
                RolePlayCharacter aimCharacter = default(RolePlayCharacter);
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == currentChrNumber) { currentCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == AimedChrNumber) { aimCharacter = chr; }
                }
                aimCharacter.IsPoisonedCharacter = false;
                currentCharacter.IsCastsMagic = true;
                currentCharacter.CurrentMana -= MinManaCost;
                currentCharacter.CanMove = !bShouldDoActionsTillCast;
                currentCharacter.CanSpeak = !BShouldSpeakTillCast;
            currentCharacter.ExpCharacter += 30;
                Console.WriteLine("Персонаж {0} потратил {1} маны и получил {4} очков опыта. Текущее количество маны: {2} из {3}", currentCharacter.NameCharacter, MinManaCost, currentCharacter.CurrentMana, currentCharacter.MaxMana,30);
                Console.WriteLine("Персонаж {0} был вылечен от отравления.", aimCharacter.NameCharacter);
            }
        }
        class ReAnimate : Spell
        {
            public ReAnimate()
            {
                MinManaCost = 150;
            Name = "Оживить";
            BShouldDoActionsTillCast = true;
                BShouldSpeakTillCast = true;
            }
            public void SpellCast(uint ourChrId)
            {
                MagicRolePlayCharacter CurrentChr = default(MagicRolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr as MagicRolePlayCharacter; break; }
                if (CurrentChr.CurrentMana >= MinManaCost)
                {

                    bool ok = false;
                    int insert = default(int);
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить заклинание \"Оживить\":", CurrentChr.NameCharacter);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.StateCharacter == (string)RolePlayCharacter.StatesForCharacter[5]) { if (chr.UnicCharacterNumber == CurrentChr.UnicCharacterNumber) Console.WriteLine("{0}.На себя <{1}>", buf, CurrentChr.StateCharacter); else Console.WriteLine("{0}.{1} <{2}>", buf, chr.NameCharacter, chr.StateCharacter); buf++; }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    if (chr.StateCharacter == (string)RolePlayCharacter.StatesForCharacter[5])
                                    {
                                        if (buf == insert)
                                        {
                                            AimCharacter = chr;
                                            ok = true;
                                            DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                            break;
                                        }
                                        else buf++;
                                    }
                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("У персонажа не хватает маны на использование этого заклинания. (требуется 150 маны)");
            }
            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                MagicRolePlayCharacter currentCharacter = default(MagicRolePlayCharacter);
                RolePlayCharacter aimCharacter = default(RolePlayCharacter);
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == currentChrNumber) { currentCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == AimedChrNumber) { aimCharacter = chr; }
                }
                aimCharacter.CurrentHP = 1;
                currentCharacter.IsCastsMagic = true;
                currentCharacter.CurrentMana -= minManaCost;
                currentCharacter.CanMove = !bShouldDoActionsTillCast;
                currentCharacter.CanSpeak = !BShouldSpeakTillCast;
            currentCharacter.ExpCharacter += minManaCost;
                Console.WriteLine("Персонаж {0} потратил {1} маны и получил {4} очков опыта. Текущее количество маны: {2} из {3}", currentCharacter.NameCharacter, MinManaCost, currentCharacter.CurrentMana, currentCharacter.MaxMana,MinManaCost);
                Console.WriteLine("Персонаж {0} был  оживлен.", aimCharacter.NameCharacter);
            }
        }
        class TakeOffParalyzed : Spell
        {
            public TakeOffParalyzed()
            {
                MinManaCost = 85;
            Name = "Отомри";
                BShouldDoActionsTillCast = false;
                BShouldSpeakTillCast = true;
            }
            public void SpellCast(uint ourChrId)
            {
                MagicRolePlayCharacter CurrentChr = default(MagicRolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr as MagicRolePlayCharacter; break; }
                if (CurrentChr.CurrentMana >= MinManaCost)
                {

                    bool ok = false;
                    int insert = default(int);
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить заклинание \"Отомри\":", CurrentChr.NameCharacter);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.IsParalyzedCharacter) { if (chr.UnicCharacterNumber == CurrentChr.UnicCharacterNumber) Console.WriteLine("{0}.На себя <{1}>", buf, CurrentChr.StateCharacter); else Console.WriteLine("{0}.{1} <{2}>", buf, chr.NameCharacter, chr.StateCharacter); buf++; }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    if (chr.IsParalyzedCharacter)
                                    {
                                        if (buf == insert)
                                        {
                                            AimCharacter = chr;
                                            ok = true;
                                            DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                            break;
                                        }
                                        else buf++;
                                    }
                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("У персонажа не хватает маны на использование этого заклинания. (требуется 150 маны)");
            }
            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                MagicRolePlayCharacter currentCharacter = default(MagicRolePlayCharacter);
                RolePlayCharacter aimCharacter = default(RolePlayCharacter);
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == currentChrNumber) { currentCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == AimedChrNumber) { aimCharacter = chr; }
                }
                aimCharacter.IsParalyzedCharacter = false;
                aimCharacter.CurrentHP = 1;
                currentCharacter.IsCastsMagic = true;
                currentCharacter.CurrentMana -= minManaCost;
                currentCharacter.CanMove = !bShouldDoActionsTillCast;
                currentCharacter.CanSpeak = !BShouldSpeakTillCast;
            currentCharacter.ExpCharacter += MinManaCost;
                Console.WriteLine("Персонаж {0} потратил {1} маны и получил {1} очков опыта. Текущее количество маны: {2} из {3}", currentCharacter.NameCharacter, MinManaCost, currentCharacter.CurrentMana, currentCharacter.MaxMana);
                Console.WriteLine("Персонаж {0} был излечен от паралича.", aimCharacter.NameCharacter);
            }
        }
        class Armor : Spell
        {
            public Armor()
            {
                MinManaCost = 50;
            Name = "Броня";
                BShouldDoActionsTillCast = false;
                BShouldSpeakTillCast = false;
            }
            public void SpellCast(uint ourChrId)
            {
                MagicRolePlayCharacter CurrentChr = default(MagicRolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr as MagicRolePlayCharacter; break; }
                if (CurrentChr.CurrentMana >= MinManaCost)
                {

                    bool ok = false;
                    int insert = default(int);
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить заклинание \"Броня\":", CurrentChr.NameCharacter);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.StateCharacter != (string)RolePlayCharacter.StatesForCharacter[5]) { if (chr.UnicCharacterNumber == CurrentChr.UnicCharacterNumber) Console.WriteLine("{0}.На себя <{1}>", buf, chr.StateCharacter); else Console.WriteLine("{0}.{1} <{2}>", buf, chr.NameCharacter, chr.StateCharacter); buf++; }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    if (chr.StateCharacter != (string)RolePlayCharacter.StatesForCharacter[5])
                                    {
                                        if (buf == insert)
                                        {
                                            AimCharacter = chr;
                                            ok = true;
                                            DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                            break;
                                        }
                                        else buf++;
                                    }
                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("У персонажа не хватает маны на использование этого заклинания с минимальной силой. (требуется минимум 50 маны)");
            }
            static void RefreshInvulnerability(object obj)
            {
                var our = obj as RolePlayCharacter;
                our.IsInvulnerable = false;
            }
            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                MagicRolePlayCharacter currentCharacter = default(MagicRolePlayCharacter);
                RolePlayCharacter aimCharacter = default(RolePlayCharacter);
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == currentChrNumber) { currentCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == AimedChrNumber) { aimCharacter = chr; }
                }
                //
                bool ok = false;
                uint maxTimeAccordingToMana = currentCharacter.CurrentMana / 50;
                while (!ok)
                {
                    try
                    {
                        Console.WriteLine("Введите силу заклинания с которой вы хотите использовать заклинание. 1 единица силы - 20 секунд неуязвимости. (Максимум = {0}):, {1}", maxTimeAccordingToMana, aimCharacter.NameCharacter);
                        uint time = default(uint);
                        time = Convert.ToUInt32(Console.ReadLine());
                        if (time > maxTimeAccordingToMana || time == 0) { Console.WriteLine("Вы ввели число, большее максимального или раное нулю. Попробуйте ещё раз."); continue; }
                        else
                        {
                            currentCharacter.CurrentMana -= time * MinManaCost;
                            currentCharacter.IsCastsMagic = true;
                            currentCharacter.CanMove = !bShouldDoActionsTillCast;
                            currentCharacter.CanSpeak = !BShouldSpeakTillCast;
                        currentCharacter.ExpCharacter += MinManaCost;
                            Console.WriteLine("Персонаж {0} потратил {1} маны, и получил {1} очков опыта. Текущее количество маны: {2} из {3}", currentCharacter.NameCharacter, maxTimeAccordingToMana * MinManaCost, currentCharacter.CurrentMana, currentCharacter.MaxMana);
                            Console.WriteLine("Персонажу {0} была дана неуязвимость на {1} секунд.", aimCharacter.NameCharacter, (time * 20), aimCharacter.CurrentHP, aimCharacter.MaxHP);
                            aimCharacter.IsInvulnerable = true;
                            TimerCallback callback = new TimerCallback(RefreshInvulnerability);
                            Timer timerRefresh = new Timer(callback, aimCharacter, (int)(time * 20000), Timeout.Infinite);
                            ok = true;

                        }
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Число слишком большое,или меньше нуля. Попробуйте ещё раз");
                        continue;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Число введено некорректно. попробуйте ещё раз.");
                        continue;
                    }
                }

            }
        }
        
        //artifacts
        abstract class Artefact : IMagic
        {
            protected uint artefactStrength;
            protected bool canUse;
            private string name;
            private bool reChargeable;
            public uint ArtefactStrength { get => artefactStrength; set => artefactStrength = value; }
            public bool CanUse { get => canUse; set => canUse = value; }
            public string Name { get => name; set => name = value; }
            public bool ReChargeable { get => reChargeable; protected set => reChargeable = value; }

            abstract public void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint Strength);
        }
        class LittleBottleWithLiveWater : Artefact
        {
            public LittleBottleWithLiveWater()
            {
                ArtefactStrength = 1;
                CanUse = true;
                ReChargeable = false;
                Name = "Маленькая бутылка с живой водой";
            }
            public void ArtefactCast(uint ourChrId)
            {
                RolePlayCharacter CurrentChr = default(RolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr; break; }

                bool ok = false;
                int insert = default(int);
                if (CanUse == true)
                {
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить артефакт \"{1}\" (восстанавливает 10 здоровья):", CurrentChr.NameCharacter, Name);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.StateCharacter != "Мертв" && chr.CurrentHP != chr.MaxHP) { if (chr.UnicCharacterNumber == CurrentChr.UnicCharacterNumber) Console.WriteLine("{0}.На себя <{1} очков здоровья из {2}>", buf, CurrentChr.CurrentHP, CurrentChr.MaxHP); else Console.WriteLine("{0}.{1} <{2} очков здоровья из {3}>", buf, chr.NameCharacter, chr.CurrentHP, chr.MaxHP); buf++; }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    if (chr.StateCharacter != "Мертв" && chr.CurrentHP != chr.MaxHP)
                                    {
                                        if (buf == insert)
                                        {
                                            AimCharacter = chr;
                                            ok = true;
                                            DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                            break;
                                        }
                                        else buf++;
                                    }
                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("Вы больше не можете использовать этот артефакт");

            }

            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                MagicRolePlayCharacter currentCharacter = null;
                RolePlayCharacter aimCharacter = null;
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == currentChrNumber) { currentCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == AimedChrNumber) { aimCharacter = chr; }
                }
                uint buf_hp = aimCharacter.CurrentHP;
                if (aimCharacter.CurrentHP + 10 > aimCharacter.MaxHP) aimCharacter.CurrentHP = aimCharacter.MaxHP;
                else aimCharacter.CurrentHP += 10;
            currentCharacter.ExpCharacter += 10;
                Console.WriteLine("Персонажу {0} было восстановлено {1} очков здоровья. Персонаж {4} получает 10 очков опыта. Текущее здоровье: {2} из {3}\nБутылка была использована.", aimCharacter.NameCharacter, aimCharacter.CurrentHP - buf_hp, aimCharacter.CurrentHP, aimCharacter.MaxHP,currentCharacter.NameCharacter);
                CanUse = false;

            }
        }
        class MiddleBottleWithLiveWater : Artefact
        {
            public MiddleBottleWithLiveWater()
            {
                ArtefactStrength = 1;
                CanUse = true;
                ReChargeable = false;
                Name = "Средняя бутылка с живой водой";
            }
            public void ArtefactCast(uint ourChrId)
            {
                RolePlayCharacter CurrentChr = default(RolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr; break; }

                bool ok = false;
                int insert = default(int);
                if (CanUse == true)
                {
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить артефакт \"{1}\"(восстанавливает 25 здоровья):", CurrentChr.NameCharacter, Name);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.StateCharacter != "Мертв" && chr.CurrentHP != chr.MaxHP) { if (chr.UnicCharacterNumber == CurrentChr.UnicCharacterNumber) Console.WriteLine("{0}.На себя <{1} очков здоровья из {2}>", buf, CurrentChr.CurrentHP, CurrentChr.MaxHP); else Console.WriteLine("{0}.{1} <{2} очков здоровья из {3}>", buf, chr.NameCharacter, chr.CurrentHP, chr.MaxHP); buf++; }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    if (chr.StateCharacter != "Мертв" && chr.CurrentHP != chr.MaxHP)
                                    {
                                        if (buf == insert)
                                        {
                                            AimCharacter = chr;
                                            ok = true;
                                            DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                            break;
                                        }
                                        else buf++;
                                    }
                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("Вы больше не можете использовать этот артефакт");

            }

            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                MagicRolePlayCharacter currentCharacter = null;
                RolePlayCharacter aimCharacter = null;
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == currentChrNumber) { currentCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == AimedChrNumber) { aimCharacter = chr; }
                }
                uint buf_hp = aimCharacter.CurrentHP;
                if (aimCharacter.CurrentHP + 25 > aimCharacter.MaxHP) aimCharacter.CurrentHP = aimCharacter.MaxHP;
                else aimCharacter.CurrentHP += 25;
            currentCharacter.ExpCharacter += 25;
            Console.WriteLine("Персонажу {0} было восстановлено {1} очков здоровья. Персонаж {4} получает 25 очков опыта. Текущее здоровье: {2} из {3}\nБутылка была использована.", aimCharacter.NameCharacter, aimCharacter.CurrentHP - buf_hp, aimCharacter.CurrentHP, aimCharacter.MaxHP, currentCharacter.NameCharacter);
            CanUse = false;

            }
        }
        class LargeBottleWithLiveWater : Artefact
        {
            public LargeBottleWithLiveWater()
            {
                ArtefactStrength = 1;
                CanUse = true;
                ReChargeable = false;
                Name = "Большая бутылка с живой водой";
            }
            public void ArtefactCast(uint ourChrId)
            {
                RolePlayCharacter CurrentChr = default(RolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr; break; }

                bool ok = false;
                int insert = default(int);
                if (CanUse == true)
                {
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить артефакт \"{1}\"(восстанавливает 50 здоровья):", CurrentChr.NameCharacter, Name);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.StateCharacter != "Мертв" && chr.CurrentHP != chr.MaxHP) { if (chr.UnicCharacterNumber == CurrentChr.UnicCharacterNumber) Console.WriteLine("{0}.На себя <{1} очков здоровья из {2}>", buf, CurrentChr.CurrentHP, CurrentChr.MaxHP); else Console.WriteLine("{0}.{1} <{2} очков здоровья из {3}>", buf, chr.NameCharacter, chr.CurrentHP, chr.MaxHP); buf++; }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    if (chr.StateCharacter != "Мертв" && chr.CurrentHP != chr.MaxHP)
                                    {
                                        if (buf == insert)
                                        {
                                            AimCharacter = chr;
                                            ok = true;
                                            DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                            break;
                                        }
                                        else buf++;
                                    }
                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("Вы больше не можете использовать этот артефакт");

            }

            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                MagicRolePlayCharacter currentCharacter = null;
                RolePlayCharacter aimCharacter = null;
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == currentChrNumber) { currentCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == AimedChrNumber) { aimCharacter = chr; }
                }
                uint buf_hp = aimCharacter.CurrentHP;
                if (aimCharacter.CurrentHP + 50 > aimCharacter.MaxHP) aimCharacter.CurrentHP = aimCharacter.MaxHP;
                else aimCharacter.CurrentHP += 50;
            currentCharacter.ExpCharacter += 50;
            Console.WriteLine("Персонажу {0} было восстановлено {1} очков здоровья. Персонаж {4} получает 50 очков опыта. Текущее здоровье: {2} из {3}\nБутылка была использована.", aimCharacter.NameCharacter, aimCharacter.CurrentHP - buf_hp, aimCharacter.CurrentHP, aimCharacter.MaxHP, currentCharacter.NameCharacter);
            CanUse = false;

            }
        }
        //
        class LittleBottleWithDeadWater : Artefact
        {
            public LittleBottleWithDeadWater()
            {
                ArtefactStrength = 1;
                ReChargeable = false;
                CanUse = true;
                Name = "Маленькая бутылка с мертвой водой";
            }
            public void ArtefactCast(uint ourChrId)
            {
                RolePlayCharacter CurrentChr = default(RolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr as RolePlayCharacter; break; }

                bool ok = false;
                int insert = default(int);
                if (CanUse == true)
                {
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить артефакт \"{1}\" (восстанавливает 10 маны):", CurrentChr.NameCharacter, Name);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (object chr in Program.CharactersList)
                        {
                            Type type = default(Type);
                            type = chr.GetType();
                            MagicRolePlayCharacter MagicChr = default(MagicRolePlayCharacter);
                            if (type.Name == "MagicRolePlayCharacter")
                            {
                                MagicChr = chr as MagicRolePlayCharacter;
                                if (MagicChr.StateCharacter != "Мертв" && MagicChr.CurrentMana != MagicChr.MaxMana) { if (MagicChr.UnicCharacterNumber == CurrentChr.UnicCharacterNumber) Console.WriteLine("{0}.На себя <{1} очков маны из {2}>", buf, MagicChr.CurrentMana, MagicChr.MaxMana); else Console.WriteLine("{0}.{1} <{2} очков маны из {3}>", buf, MagicChr.NameCharacter, MagicChr.CurrentMana, MagicChr.MaxMana); buf++; }
                            }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                MagicRolePlayCharacter AimCharacter = default(MagicRolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    Type type = default(Type);
                                    type = chr.GetType();
                                    MagicRolePlayCharacter MagicChr = default(MagicRolePlayCharacter);
                                    if (type.Name == "MagicRolePlayCharacter")
                                    {
                                        MagicChr = chr as MagicRolePlayCharacter;
                                        if (MagicChr.StateCharacter != "Мертв" && MagicChr.CurrentMana != MagicChr.MaxMana)
                                        {
                                            if (buf == insert)
                                            {
                                                AimCharacter = chr as MagicRolePlayCharacter;
                                                ok = true;
                                                DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                                break;
                                            }
                                            else buf++;
                                        }
                                    }

                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("Вы больше не можете использовать этот артефакт");

            }

            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                RolePlayCharacter currentCharacter = null;
                MagicRolePlayCharacter aimCharacter = null;
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == AimedChrNumber) { aimCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == currentChrNumber) { currentCharacter = chr; }
                }
                uint buf_mana = aimCharacter.CurrentMana;
                if (aimCharacter.CurrentMana + 10 > aimCharacter.MaxMana) aimCharacter.CurrentMana = aimCharacter.MaxMana;
                else aimCharacter.CurrentMana += 10;
            currentCharacter.ExpCharacter += 10;
                Console.WriteLine("Персонажу {0} было восстановлено {1} очков маны. Текущая мана: {2} из {3}. Персонаж {4} получат 10 очков опыта\nБутылка была использована.", aimCharacter.NameCharacter, aimCharacter.CurrentMana - buf_mana, aimCharacter.CurrentMana, aimCharacter.CurrentMana,currentCharacter.NameCharacter);
                CanUse = false;

            }
        }
        class MiddleBottleWithDeadWater : Artefact
        {
            public MiddleBottleWithDeadWater()
            {
                ArtefactStrength = 1;
                CanUse = true;
                ReChargeable = false;
                Name = "Средняя бутылка с мертвой водой";
            }
            public void ArtefactCast(uint ourChrId)
            {
                RolePlayCharacter CurrentChr = default(RolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr as RolePlayCharacter; break; }

                bool ok = false;
                int insert = default(int);
                if (CanUse == true)
                {
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить артефакт \"{1}\" (восстанавливает 25 маны):", CurrentChr.NameCharacter, Name);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (object chr in Program.CharactersList)
                        {
                            Type type = default(Type);
                            type = chr.GetType();
                            MagicRolePlayCharacter MagicChr = default(MagicRolePlayCharacter);
                            if (type.Name == "MagicRolePlayCharacter")
                            {
                                MagicChr = chr as MagicRolePlayCharacter;
                                if (MagicChr.StateCharacter != "Мертв" && MagicChr.CurrentMana != MagicChr.MaxMana) { if (MagicChr.UnicCharacterNumber == CurrentChr.UnicCharacterNumber) Console.WriteLine("{0}.На себя <{1} очков маны из {2}>", buf, MagicChr.CurrentMana, MagicChr.MaxMana); else Console.WriteLine("{0}.{1} <{2} очков маны из {3}>", buf, MagicChr.NameCharacter, MagicChr.CurrentMana, MagicChr.MaxMana); buf++; }
                            }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                MagicRolePlayCharacter AimCharacter = default(MagicRolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    Type type = default(Type);
                                    type = chr.GetType();
                                    MagicRolePlayCharacter MagicChr = default(MagicRolePlayCharacter);
                                    if (type.Name == "MagicRolePlayCharacter")
                                    {
                                        MagicChr = chr as MagicRolePlayCharacter;
                                        if (MagicChr.StateCharacter != "Мертв" && MagicChr.CurrentMana != MagicChr.MaxMana)
                                        {
                                            if (buf == insert)
                                            {
                                                AimCharacter = chr as MagicRolePlayCharacter;
                                                ok = true;
                                                DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                                break;
                                            }
                                            else buf++;
                                        }
                                    }

                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("Вы больше не можете использовать этот артефакт");

            }

            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                RolePlayCharacter currentCharacter = null;
                MagicRolePlayCharacter aimCharacter = null;
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == AimedChrNumber) { aimCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == currentChrNumber) { currentCharacter = chr; }
                }
                uint buf_mana = aimCharacter.CurrentMana;
                if (aimCharacter.CurrentMana + 25 > aimCharacter.MaxMana) aimCharacter.CurrentMana = aimCharacter.MaxMana;
                else aimCharacter.CurrentMana += 25;
            currentCharacter.ExpCharacter += 25;
            Console.WriteLine("Персонажу {0} было восстановлено {1} очков маны. Текущая мана: {2} из {3}. Персонаж {4} получат 25 очков опыта\nБутылка была использована.", aimCharacter.NameCharacter, aimCharacter.CurrentMana - buf_mana, aimCharacter.CurrentMana, aimCharacter.CurrentMana, currentCharacter.NameCharacter);
            CanUse = false;
            }
        }
        class LargeBottleWithDeadWater : Artefact
        {
            public LargeBottleWithDeadWater()
            {
                ArtefactStrength = 1;
                CanUse = true;
                ReChargeable = false;
                Name = "Большая бутылка с мертвой водой";
            }
            public void ArtefactCast(uint ourChrId)
            {
                RolePlayCharacter CurrentChr = default(RolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr as RolePlayCharacter; break; }

                bool ok = false;
                int insert = default(int);
                if (CanUse == true)
                {
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить артефакт \"{1}\" (восстанавливает 50 маны):", CurrentChr.NameCharacter, Name);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (object chr in Program.CharactersList)
                        {
                            Type type = default(Type);
                            type = chr.GetType();
                            MagicRolePlayCharacter MagicChr = default(MagicRolePlayCharacter);
                            if (type.Name == "MagicRolePlayCharacter")
                            {
                                MagicChr = chr as MagicRolePlayCharacter;
                                if (MagicChr.StateCharacter != "Мертв" && MagicChr.CurrentMana != MagicChr.MaxMana) { if (MagicChr.UnicCharacterNumber == CurrentChr.UnicCharacterNumber) Console.WriteLine("{0}.На себя <{1} очков маны из {2}>", buf, MagicChr.CurrentMana, MagicChr.MaxMana); else Console.WriteLine("{0}.{1} <{2} очков маны из {3}>", buf, MagicChr.NameCharacter, MagicChr.CurrentMana, MagicChr.MaxMana); buf++; }
                            }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                MagicRolePlayCharacter AimCharacter = default(MagicRolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    Type type = default(Type);
                                    type = chr.GetType();
                                    MagicRolePlayCharacter MagicChr = default(MagicRolePlayCharacter);
                                    if (type.Name == "MagicRolePlayCharacter")
                                    {
                                        MagicChr = chr as MagicRolePlayCharacter;
                                        if (MagicChr.StateCharacter != "Мертв" && MagicChr.CurrentMana != MagicChr.MaxMana)
                                        {
                                            if (buf == insert)
                                            {
                                                AimCharacter = chr as MagicRolePlayCharacter;
                                                ok = true;
                                                DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                                break;
                                            }
                                            else buf++;
                                        }
                                    }

                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("Вы больше не можете использовать этот артефакт");

            }

            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                RolePlayCharacter currentCharacter = null;
                MagicRolePlayCharacter aimCharacter = null;
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == AimedChrNumber) { aimCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == currentChrNumber) { currentCharacter = chr; }
                }
                uint buf_mana = aimCharacter.CurrentMana;
                if (aimCharacter.CurrentMana + 50 > aimCharacter.MaxMana) aimCharacter.CurrentMana = aimCharacter.MaxMana;
                else aimCharacter.CurrentMana += 50;
            currentCharacter.ExpCharacter += 50;
            Console.WriteLine("Персонажу {0} было восстановлено {1} очков маны. Текущая мана: {2} из {3}. Персонаж {4} получат 50 очков опыта\nБутылка была использована.", aimCharacter.NameCharacter, aimCharacter.CurrentMana - buf_mana, aimCharacter.CurrentMana, aimCharacter.CurrentMana, currentCharacter.NameCharacter);
            CanUse = false;

            }
        }
        //
        class StaffOfLightning : Artefact
        {
            public StaffOfLightning()
            {
                ArtefactStrength = 1;
                CanUse = true;
                ReChargeable = true;

                Name = "Посох молнии";
            }
            //
            public void ArtefactCast(uint ourChrId)
            {
                MagicRolePlayCharacter CurrentChr = default(MagicRolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr as MagicRolePlayCharacter; break; }


                bool ok = false;
                int insert = default(int);
                if (CurrentChr.CurrentMana > 0) CanUse = true;
                else CanUse = false;
                if (CanUse)
                {
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить артефакт \"{1}\":", CurrentChr.NameCharacter, Name);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.StateCharacter != "Мертв") { if (chr.UnicCharacterNumber != CurrentChr.UnicCharacterNumber) { Console.WriteLine("{0}.{1} <{2} очков здоровья из {3}>", buf, chr.NameCharacter, chr.CurrentHP, chr.MaxHP); buf++; } }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    if (chr.StateCharacter != "Мертв" && chr.UnicCharacterNumber != CurrentChr.UnicCharacterNumber)
                                    {
                                        if (buf == insert)
                                        {
                                            AimCharacter = chr;
                                            ok = true;
                                            DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                            break;
                                        }
                                        else buf++;
                                    }
                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("Вам не хватает маны для использования данного артефакта.");
            }

            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                MagicRolePlayCharacter currentCharacter = null;
                RolePlayCharacter aimCharacter = null;
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == currentChrNumber) { currentCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == AimedChrNumber) { aimCharacter = chr; }
                }
                bool ok = false;
                uint maxHPAccordingToMana = currentCharacter.CurrentMana;
                uint maxHPAccordingToAimHP = aimCharacter.CurrentHP;
                uint MaxHPToDec = Math.Min(maxHPAccordingToAimHP, maxHPAccordingToMana);


                uint hp = default(uint);
                while (!ok)
                {
                    Console.WriteLine("У этого персонажа {0} единиц здоровья из {1}", aimCharacter.CurrentHP, aimCharacter.MaxHP);
                    try
                    {
                        Console.WriteLine("Введите число очков здоровья, которое {0} отнимет у {1} (Максимум = {2}):", currentCharacter.NameCharacter, aimCharacter.NameCharacter, MaxHPToDec);
                        hp = Convert.ToUInt32(Console.ReadLine());
                        if (hp > MaxHPToDec) { Console.WriteLine("Вы ввели число, большее максимальнго. Попробуйте ещё раз."); continue; }
                        else
                        {
                            ok = true;
                            currentCharacter.CurrentMana -= hp;
                            if (aimCharacter.IsInvulnerable == false)
                            {
                                uint buf_hp = default(uint);
                                buf_hp = aimCharacter.CurrentHP;
                                if (aimCharacter.CurrentHP - hp < 0) aimCharacter.CurrentHP = 0;
                                else aimCharacter.CurrentHP -= hp;
                            currentCharacter.ExpCharacter += hp;
                                Console.WriteLine("Персонаж {0} потратил {1} маны. Текущее количество маны: {2} из {3}", currentCharacter.NameCharacter, hp, currentCharacter.CurrentMana, currentCharacter.MaxMana);
                                 Console.WriteLine("Персонаж {0} получает {1} очков опыта.", currentCharacter.NameCharacter, hp);

                            Console.WriteLine("Персонаж {0} потерял {1} очков здоровья. Текущее здоровье: {2} из {3}", aimCharacter.NameCharacter, buf_hp - aimCharacter.CurrentHP, aimCharacter.CurrentHP, aimCharacter.MaxHP);

                            }
                            else
                            {
                                Console.WriteLine("Перонаж {0} применил артефакт \"{1}\" на {2}, но оказалось что он неузвим. Урон нанесен не будет.", currentCharacter.NameCharacter, Name, aimCharacter.NameCharacter);
                                Console.WriteLine("Персонаж {0} потратил {1} маны. Текущее количество маны: {2} из {3}", currentCharacter.NameCharacter, hp, currentCharacter.CurrentMana, currentCharacter.MaxMana);

                            }


                        }
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Число слишком большое,или меньше нуля. Попробуйте ещё раз");
                        continue;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Число введено некорректно. попробуйте ещё раз.");
                        continue;
                    }
                }

            }

        }
        class FrogDeck : Artefact
        {
            public FrogDeck()
            {
                ArtefactStrength = 1;
                CanUse = true;
                ReChargeable = false;
                Name = "Декокт из лягушачих лапок";
            }
            public void ArtefactCast(uint ourChrId)
            {
                RolePlayCharacter CurrentChr = default(RolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr as RolePlayCharacter; break; }

                bool ok = false;
                int insert = default(int);
                if (CanUse == true)
                {
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить артефакт \"{1}\" (лечит ототравления):", CurrentChr.NameCharacter, Name);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.IsPoisonedCharacter) { if (chr.UnicCharacterNumber == CurrentChr.UnicCharacterNumber) Console.WriteLine("{0}.На себя <{1}>", buf, CurrentChr.StateCharacter); else Console.WriteLine("{0}.{1} <{2}>", buf, chr.NameCharacter, chr.StateCharacter); buf++; }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    if (chr.IsPoisonedCharacter)
                                    {
                                        if (buf == insert)
                                        {
                                            AimCharacter = chr;
                                            ok = true;
                                            DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                            break;
                                        }
                                        else buf++;
                                    }
                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("Вы больше не можете использовать этот артефакт");

            }

            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                RolePlayCharacter currentCharacter = null;
                RolePlayCharacter aimCharacter = null;
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == currentChrNumber) { currentCharacter = chr; }
                    if (chr.UnicCharacterNumber == AimedChrNumber) { aimCharacter = chr; }
                }
                aimCharacter.IsPoisonedCharacter = false;
            currentCharacter.ExpCharacter += 30;
                Console.WriteLine("Персонаж {0} был вылечен от отравления. Персонаж {1} получает 30 очков опыта. Декокт был использован.", aimCharacter.NameCharacter,currentCharacter.NameCharacter);
                CanUse = false;
            }
        }
        class PoisonousSaliva : Artefact
        {
            public PoisonousSaliva()
            {
                ArtefactStrength = 1;
                CanUse = true;
                ReChargeable = true;
                Name = "Ядовитая слюна";
            }
            //
            public void ArtefactCast(uint ourChrId)
            {
                MagicRolePlayCharacter CurrentChr = default(MagicRolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr as MagicRolePlayCharacter; break; }


                bool ok = false;
                int insert = default(int);
                if (CurrentChr.CurrentMana > 0) CanUse = true;
                else CanUse = false;
                if (CanUse)
                {
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить артефакт \"{1}\"(отниает очки здороья и отравляет):", CurrentChr.NameCharacter, Name);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.StateCharacter != "Мертв") { if (chr.UnicCharacterNumber != CurrentChr.UnicCharacterNumber) { Console.WriteLine("{0}.{1} <{2} очков здоровья из {3}>", buf, chr.NameCharacter, chr.CurrentHP, chr.MaxHP); buf++; } }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    if (chr.StateCharacter != "Мертв" && chr.UnicCharacterNumber != CurrentChr.UnicCharacterNumber)
                                    {
                                        if (buf == insert)
                                        {
                                            AimCharacter = chr;
                                            ok = true;
                                            DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                            break;
                                        }
                                        else buf++;
                                    }
                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("Вам не хватает маны для использования данного артефакта.");
            }

            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                MagicRolePlayCharacter currentCharacter = null;
                RolePlayCharacter aimCharacter = null;
                Type type_object = default(Type);
                foreach (object chr in Program.CharactersList)
                {
                    object buf = chr;
                    type_object = buf.GetType();
                    if (type_object.Name == "MagicRolePlayCharacter")
                    {
                        MagicRolePlayCharacter mg_character = chr as MagicRolePlayCharacter;
                        if (mg_character.UnicCharacterNumber == currentChrNumber) { currentCharacter = mg_character; }
                    }
                }
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == AimedChrNumber) { aimCharacter = chr; }
                }
                bool ok = false;
                uint maxHPAccordingToMana = currentCharacter.CurrentMana;
                uint maxHPAccordingToAimHP = aimCharacter.CurrentHP;
                uint MaxHPToDec = Math.Min(maxHPAccordingToAimHP, maxHPAccordingToMana);


                uint hp = default(uint);
                while (!ok)
                {
                    Console.WriteLine("У этого персонажа {0} единиц здоровья из {1}", aimCharacter.CurrentHP, aimCharacter.MaxHP);
                    try
                    {
                        Console.WriteLine("Введите число очков здоровья, которое {0} отнимет у {1} (Максимум = {2}):", currentCharacter.NameCharacter, aimCharacter.NameCharacter, MaxHPToDec);
                        hp = Convert.ToUInt32(Console.ReadLine());
                        if (hp > MaxHPToDec) { Console.WriteLine("Вы ввели число, большее максимальнго. Попробуйте ещё раз."); continue; }
                        else
                        {

                            ok = true;
                            currentCharacter.CurrentMana -= hp;
                            if (aimCharacter.IsInvulnerable == false)
                            {
                                uint buf_hp = default(uint);
                                buf_hp = aimCharacter.CurrentHP;
                                if (aimCharacter.CurrentHP - hp < 0) aimCharacter.CurrentHP = 0;
                                else aimCharacter.CurrentHP -= hp;
                                aimCharacter.IsPoisonedCharacter = true;
                                 currentCharacter.ExpCharacter += hp;
                                Console.WriteLine("Персонаж {0} потратил {1} маны, и получил {1} очков опыта. Текущее количество маны: {2} из {3}", currentCharacter.NameCharacter, hp, currentCharacter.CurrentMana, currentCharacter.MaxMana);
                                Console.WriteLine("Персонаж {0} потерял {1} очков здоровья, и был отравлен Текущее здоровье: {2} из {3}", aimCharacter.NameCharacter, buf_hp - aimCharacter.CurrentHP, aimCharacter.CurrentHP, aimCharacter.MaxHP);

                            }
                            else
                            {
                                Console.WriteLine("Перонаж {0} применил артефакт \"{1}\" на {2}, но оказалось что он неузвим. Урон и отравление нанесено не будет", currentCharacter.NameCharacter, Name, aimCharacter.NameCharacter);
                                Console.WriteLine("Персонаж {0} потратил {1} маны. Текущее количество маны: {2} из {3}", currentCharacter.NameCharacter, hp, currentCharacter.CurrentMana, currentCharacter.MaxMana);

                            }


                        }
                    }
                    catch (OverflowException)
                    {
                        Console.WriteLine("Число слишком большое,или меньше нуля. Попробуйте ещё раз");
                        continue;
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Число введено некорректно. попробуйте ещё раз.");
                        continue;
                    }
                }

            }

        }
        class BasiliskEye : Artefact
        {
            public BasiliskEye()
            {
                ArtefactStrength = 1;
                CanUse = true;
                ReChargeable = false;
                Name = "Глаз василиска";
            }
            public void ArtefactCast(uint ourChrId)
            {
                RolePlayCharacter CurrentChr = default(RolePlayCharacter);
                foreach (RolePlayCharacter chr in Program.CharactersList)
                    if (chr.UnicCharacterNumber == ourChrId) { CurrentChr = chr as RolePlayCharacter; break; }

                bool ok = false;
                int insert = default(int);
                if (CanUse == true)
                {
                    while (!ok)
                    {
                        Console.WriteLine("Выберите персонажа на которого {0} должен применить артефакт \"{1}\" (парализует):", CurrentChr.NameCharacter, Name);
                        int buf = 1;
                        Console.WriteLine("0: Выход.");
                        foreach (RolePlayCharacter chr in Program.CharactersList)
                        {
                            if (chr.CurrentHP > 0) { if (chr.UnicCharacterNumber != CurrentChr.UnicCharacterNumber) { Console.WriteLine("{0}.{1} <{2}>", buf, chr.NameCharacter, chr.StateCharacter); buf++; } }
                        }
                        try
                        {
                            insert = Convert.ToInt32(Console.ReadLine());
                            if (insert > buf || insert < 0) { Console.WriteLine("Такого пункта не существует. попробуйте ещё раз."); continue; }
                            else if (insert == 0) { ok = true; }
                            else
                            {
                                RolePlayCharacter AimCharacter = default(RolePlayCharacter);
                                buf = 1;
                                foreach (RolePlayCharacter chr in Program.CharactersList)
                                {
                                    if (chr.CurrentHP > 0 && chr.UnicCharacterNumber != CurrentChr.UnicCharacterNumber)
                                    {
                                        if (buf == insert)
                                        {
                                            AimCharacter = chr;
                                            ok = true;
                                            DoMagicAction(CurrentChr.UnicCharacterNumber, AimCharacter.UnicCharacterNumber);
                                            break;
                                        }
                                        else buf++;
                                    }
                                }
                            }
                        }
                        catch (FormatException) { Console.WriteLine("Число введено неверно. Попробуйте ещё раз."); continue; }
                    }
                }
                else Console.WriteLine("Вы больше не можете использовать этот артефакт");

            }

            public override void DoMagicAction(uint currentChrNumber, uint AimedChrNumber, uint MagicStrength = 1)
            {
                Program.RefreshCastingAbilities();
                RolePlayCharacter currentCharacter = null;
                RolePlayCharacter aimCharacter = null;
                foreach (RolePlayCharacter chr in Program.CharactersList)
                {
                    if (chr.UnicCharacterNumber == currentChrNumber) { currentCharacter = chr; }
                    if (chr.UnicCharacterNumber == AimedChrNumber) { aimCharacter = chr; }
                }
                if (aimCharacter.IsInvulnerable == false)
                {
                    aimCharacter.IsParalyzedCharacter = true;
                currentCharacter.ExpCharacter += 100;
                    Console.WriteLine("Персонаж {0} был парализован. Персонаж {1} получает 100 очков опыта. Глаз василиска был использован.", aimCharacter.NameCharacter,currentCharacter.NameCharacter);
                }
                else Console.WriteLine("Артефакт был использован на {0}, но оказалось, что он неуязвим. Глаз василиска был использован.");
                CanUse = false;
            }
        }
    //

    class Program
    {
        static public ArrayList CharacterNames = new ArrayList() {"Abriell","Adair","Adara","Adriel","Aiyana","Alissa","Alixandra","Altair","Amara","Anatol","Anya","Arcadia","Ariadne","Arianwen","Aurelia","Aurelian","Aurelius","Avalon","Acalia","Alaire","Auristela","Bastian","Breena","Brielle","Briallan","Briseis","Cambria","Cara","Carys","Caspian","Cassia","Cassiel","Cassiopeia","Cassius","Chaniel","Cora","Corbin","Cyprian","Daire","Darius","Destin","Drake","Drystan","Dagen","Devlin","Devlyn","Eira","Eirian","Elysia","Eoin","Evadne","Eliron","Evanth","Fineas","Finian","Fyodor","Gareth","Gavriel","Griffin","Guinevere","Gaerwn","Ginerva","Hadriel","Hannelore","Hermione","Hesperos","Lagan","Ianthe","Ignacia","Ignatius","Iseult","Isolde","Jessalyn","Kara","Kerensa","Korbin","Kyler","Kyra","Katriel","Kyrielle","Leala","Leila","Lilith","Liora","Lucien","Lyra","Leira","Liriene","Liron","Maia","Marius","Mathieu","Mireille","Mireya","Maylea","Meira","Natania","Nerys","Nuriel","Nyssa","Neirin","Nyfain","Oisin","Oralie","Orion","Orpheus","Ozara","Oleisa","Orinthea","Peregrine","Persephone","Perseus","Petronela","Phelan","Pryderi","Pyralia","Pyralis","Qadira","Quintessa","Quinevere","Raisa","Remus","Rhyan","Rhydderch","Riona","Renfrew","Saoirse","Sarai","Sebastian","Seraphim","Seraphina","Sirius","Sorcha","Saira","Sarielle","Serian","Séverin","Tavish","Tearlach","Terra","Thalia","Thaniel","Theia","Torian","Torin","Tressa","Tristana","Uriela","Urien","Ulyssia","Vanora","Vasilis","Xanthus","Xara","Xylia","Yadira","Yseult","Yakira","Yeira","Yeriel","Yestin","Zaira","Zephyr","Zora","Zorion","Zaniel","Zarek"};
            static public void RefreshInfo()
        {
            foreach (object chr in CharactersList)
            {
                Type type = default(Type);
                type = chr.GetType();
                if (type.Name == "RolePlayCharacter")
                {
                    RolePlayCharacter obj = chr as RolePlayCharacter;
                    obj.StartStateTask();
                }
                else
                {
                    MagicRolePlayCharacter obj = chr as MagicRolePlayCharacter;
                    obj.StartStateTask();
                }
            }
        }
            static public ArrayList CharactersList;
            public static void RefreshCastingAbilities()
            {
                Type objType = default(Type);
                foreach (object chr in CharactersList)
                {
                    objType = chr.GetType();
                    if (objType.Name == "MagicRolePlayCharacter") { MagicRolePlayCharacter obj = chr as MagicRolePlayCharacter; obj.IsCastsMagic = false; }
                }
            }
            static void Main(string[] args)
            {
                bool exit = false;
                ConsoleKeyInfo key;
                CharactersList = new ArrayList();
                while (!exit)
                {
                    Console.Clear();
                    Console.WriteLine("Выберите действие:\n1.Создать нового персонажа\n2.Вывести информацию о всех персонажа\n3.Выбрать персонажа для дальнейших действий\n4.Выход");
                    key = Console.ReadKey();
                    Console.WriteLine();
                    switch (key.KeyChar)
                    {
                        case '1':
                            {
                            Console.WriteLine("0 - Выход\n1 - Создать обычного персонажа\n2 - Создать персонажа владеющего магией");
                            key = Console.ReadKey();
                            Console.WriteLine();
                            switch (key.KeyChar)
                            {
                                case '0':
                                    {
                                        break;
                                    }
                                case '1':
                                    {
                                        CharactersList.Add(new RolePlayCharacter());
                                        break;
                                    }
                                case '2':
                                    {
                                        CharactersList.Add(new MagicRolePlayCharacter());
                                        break;
                                    }
                                default:
                                    {
                                        Console.WriteLine("Такого пункта нет!!");
                                        break;
                                    }
                            }
                            RefreshInfo();
                                Console.ReadKey();
                                RefreshInfo();
                                break;
                            }

                        case '2':
                            {
                            RefreshInfo();
                            foreach (RolePlayCharacter chr in CharactersList)
                                {
                                    Console.WriteLine(chr.ToString());
                                }
                                Console.ReadKey();
                            RefreshInfo();
                            break;
                            }
                        case '3':
                            {
                            RefreshInfo();
                            Type type = default(Type);
                            for (int i = 0; i < CharactersList.Count; i++)
                                {
                                    object obj = CharactersList[i];
                                type = obj.GetType();
                                if (type.Name == "RolePlayCharacter")
                                {
                                    RolePlayCharacter chr = obj as RolePlayCharacter;
                                    Console.WriteLine("{0}.{1} <Обычный>", i + 1, chr.NameCharacter);
                                }
                                else
                                {
                                    MagicRolePlayCharacter chr = obj as MagicRolePlayCharacter;
                                    Console.WriteLine("{0}.{1} <Персонаж с магией>", i + 1, chr.NameCharacter);
                                }
                                }
                                int key_bf = default(int);
                                bool ok = default(bool);
                                while (!ok)
                                {
                                try
                                {
                                    key_bf = Convert.ToInt32(Console.ReadLine());
                                    key_bf -= 1;
                                    if (key_bf >= 0 && key_bf < CharactersList.Count) ok = true;
                                    else Console.WriteLine("Введен некорректный номер, попробуйте ещё раз");
                                }
                                catch (FormatException) { continue; }

                                }
                                bool ex = false;
                            object buf_obj = CharactersList[key_bf];
                            type = buf_obj.GetType();
                            if (type.Name == "RolePlayCharacter")
                            {
                                RolePlayCharacter currentChr = buf_obj as RolePlayCharacter;
                                if (currentChr.CurrentHP != 0)
                                {
                                    if (!currentChr.IsParalyzedCharacter)
                                    {
                                        while (!ex)
                                        {
                                            if (currentChr.CurrentHP == 0 || currentChr.IsParalyzedCharacter) { ex = true; continue; }
                                            ConsoleKeyInfo key_choise;
                                            Console.Clear();
                                            Console.WriteLine("Выберите действие персонажа {0}:\n0-Выход\n1-Посмотреть информацию о персонаже.\n2-Сравнить персонажа с другим персонажем\n3-Артефакты..\n4-Взаимодействие персонажей", currentChr.NameCharacter);
                                            try
                                            {
                                                key_choise = Console.ReadKey();
                                                switch (key_choise.KeyChar)
                                                {
                                                    case '0': { ex = true;  break; }
                                                    case '1': { RefreshInfo(); Console.WriteLine(currentChr.ToString()); Console.ReadKey(); break; }
                                                    case '2': { currentChr.Compareing(); Console.ReadKey(); break; }
                                                    case '3':
                                                        {
                                                            bool ex_arts = false;
                                                            while (!ex_arts)
                                                            {
                                                                if (currentChr.CurrentHP == 0 || currentChr.IsParalyzedCharacter) { ex_arts = true; continue; }
                                                                Console.Clear();
                                                                Console.WriteLine("Выберите действие с артефактами для персонажа {0}:\n0-Выход\n1-Посмотреть инвентарь персонажа.\n2-Положить артефакт в инвентарь\n3-Выбросить артефакт из инвентаря\n4-Передать артефакт другому персонажу\n5-Использовать артефакт", currentChr.NameCharacter);
                                                                key_choise = Console.ReadKey();
                                                                Console.WriteLine();
                                                                switch (key_choise.KeyChar)
                                                                {
                                                                    case '0': { ex_arts = true;  break; }
                                                                    case '1': { currentChr.ShowInventory(); Console.ReadKey(); break; }
                                                                    case '2': { currentChr.AddArtefactToInventory(); Console.ReadKey(); break; }
                                                                    case '3': { currentChr.DeleteArtefactFromInventory(); Console.ReadKey(); break; }
                                                                    case '4': { currentChr.GiveArtefact(); Console.ReadKey(); break; }
                                                                    case '5': { currentChr.UseArtefact(); Console.ReadKey(); break; }
                                                                }
                                                            }
                                                            break;
                                                        }
                                                    case '4':
                                                        {
                                                            bool ex_actions = false;
                                                            while (!ex_actions)
                                                            {
                                                                if (currentChr.CurrentHP == 0 || currentChr.IsParalyzedCharacter) { ex_actions = true; continue; }

                                                                Console.Clear();
                                                                Console.WriteLine("Выберите взаимодействие для персонажа {0}:\n0-Выход\n1-Поговорить с персонажем.\n2-Сразится с персонажем", currentChr.NameCharacter);
                                                                key_choise = Console.ReadKey();
                                                                Console.WriteLine();
                                                                switch (key_choise.KeyChar)
                                                                {
                                                                    case '0': { ex_actions = true; break; }
                                                                    case '1': { currentChr.SpeakWithCharacter(); Console.ReadKey(); break; }
                                                                    case '2': { currentChr.FightWithCharacter(); Console.ReadKey(); break; }


                                                                }
                                                            }
                                                            break;
                                                        }
                                                }
                                            }
                                            catch (ArgumentException) { continue; }
                                        }
                                    }
                                    else Console.WriteLine("Этот персонаж парализован.");
                                }
                                else Console.WriteLine("Этот персонаж мертв.");
                            }
                            else
                            {
                                MagicRolePlayCharacter currentChr = buf_obj as MagicRolePlayCharacter;

                                if (currentChr.CurrentHP != 0)
                                {
                                    if (!currentChr.IsParalyzedCharacter)
                                    {
                                        while (!ex)
                                        {

                                            if (currentChr.CurrentHP == 0 || currentChr.IsParalyzedCharacter) { ex = true; continue; }
                                            ConsoleKeyInfo key_choise;
                                            Console.Clear();
                                            Console.WriteLine("Выберите действие для персонажа {0}:\n0-Выход\n1-Посмотреть информацию о персонаже.\n2-Сравнить персонажа с другим персонажем\n3-Артефакты..\n4-Магия..\n5-Взаимодействие с персонажами", currentChr.NameCharacter);
                                            try
                                            {
                                                key_choise = Console.ReadKey();
                                                switch (key_choise.KeyChar)
                                                {
                                                    case '0': { ex = true; Console.ReadKey(); break; }
                                                    case '1': { RefreshInfo(); Console.WriteLine(currentChr.ToString()); Console.ReadKey(); break; }
                                                    case '2': { currentChr.Compareing(); Console.ReadKey(); break; }
                                                    case '3':
                                                        {
                                                            bool ex_arts = false;
                                                            while (!ex_arts)
                                                            {
                                                                if (currentChr.CurrentHP == 0 || currentChr.IsParalyzedCharacter) { ex_arts = true; continue; }

                                                                Console.Clear();
                                                                Console.WriteLine("Выберите действие с артефактами для персонажа {0}:\n0-Выход\n1-Посмотреть инвентарь персонажа.\n2-Положить артефакт в инвентарь\n3-Выбросить артефакт из инвентаря\n4-Передать артефакт другому персонажу\n5-Использовать артефакт", currentChr.NameCharacter);
                                                                key_choise = Console.ReadKey();
                                                                Console.WriteLine();
                                                                switch (key_choise.KeyChar)
                                                                {
                                                                    case '0': { ex_arts = true; break; }
                                                                    case '1': { currentChr.ShowInventory(); Console.ReadKey(); break; }
                                                                    case '2': { currentChr.AddArtefactToInventory(); Console.ReadKey(); break; }
                                                                    case '3': { currentChr.DeleteArtefactFromInventory(); Console.ReadKey(); break; }
                                                                    case '4': { currentChr.GiveArtefact(); Console.ReadKey(); break; }
                                                                    case '5': { currentChr.UseArtefact(); Console.ReadKey(); break; }
                                                                }
                                                            }
                                                            break;
                                                        }
                                                    case '4':
                                                        {
                                                            bool ex_magic = false;
                                                            while (!ex_magic)
                                                            {
                                                                if (currentChr.CurrentHP == 0 || currentChr.IsParalyzedCharacter) { ex_magic = true; continue; }

                                                                Console.Clear();
                                                                Console.WriteLine("Выберите действие с магией для персонажа {0}:\n0-Выход\n1-Выучить заклинание\n2-Забыть заклинание\n3-Произнести заклинание\n4-Заклинания, которые знает персонаж", currentChr.NameCharacter);
                                                                key_choise = Console.ReadKey();
                                                                Console.WriteLine();
                                                                switch (key_choise.KeyChar)
                                                                {
                                                                    case '0': { ex_magic = true; break; }
                                                                    case '1': { currentChr.LearnSpell(); Console.ReadKey(); break; }
                                                                    case '2': { currentChr.ForgetSpell(); Console.ReadKey(); break; }
                                                                    case '3': { currentChr.UseSpell(); Console.ReadKey(); break; }
                                                                    case '4': { currentChr.ShowSpellBook(); Console.ReadKey(); break; }

                                                                }
                                                            }
                                                            break;
                                                        }
                                                    case '5':
                                                        {
                                                            bool ex_actions = false;
                                                            while (!ex_actions)
                                                            {
                                                                if (currentChr.CurrentHP == 0 || currentChr.IsParalyzedCharacter) { ex_actions = true; continue; }

                                                                Console.Clear();
                                                                Console.WriteLine("Выберите взаимодействие для персонажа {0}:\n0-Выход\n1-Поговорить с персонажем.\n2-Сразиться с персонажем", currentChr.NameCharacter);
                                                                key_choise = Console.ReadKey();
                                                                Console.WriteLine();
                                                                switch (key_choise.KeyChar)
                                                                {
                                                                    case '0': { ex_actions = true; break; }
                                                                    case '1': { currentChr.SpeakWithCharacter(); Console.ReadKey(); break; }
                                                                    case '2': { currentChr.FightWithCharacter(); Console.ReadKey(); break; }

                                                                }
                                                            }
                                                            break;
                                                        }
                                                }
                                            }
                                            catch (ArgumentException) { continue; }
                                        }
                                    }
                                    else Console.WriteLine("Этотперсонаж парализован");
                                }
                                else Console.WriteLine("Этот персонаж мертв");
                            }
                                Console.ReadKey();
                                RefreshInfo();
                                  break;
                            }
                    case '4': { exit = true; break; }
                       
                        default:
                            {
                                Console.WriteLine("Такого пункта нет!");
                              
                            Console.ReadKey();
                                break;
                            }
                    }
                }
              
            }
        }
    }

