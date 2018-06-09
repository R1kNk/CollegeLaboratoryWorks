
#include "stdafx.h"
#include <iostream>
#include <string>
#include <map>
#include <cctype>
#include <fstream>
#include "windows.h"
using namespace std;

map <string, unsigned char> morzeMap = {
	{ ".-", '�' },
	{ "-...", '�' },
	{ ".--",'�' },
	{ "--.",'�' },
	{ "-..",'�' },
	{ ".", '�' },
	{ "...-",'�' },
	{ "--..",'�' },
	{ "..",'�' },
	{ ".---", '�' },
	{ "-.-",'�' },
	{ ".-..",'�' },
	{ "--",'�' },
	{ "-.",'�' },
	{ "---" ,'�' },
	{ ".--.",'�' },
	{ ".-.",'�' },
	{ "...",'�' },
	{ "-",'�' },
	{ "..-",'�' },
	{ "..-.",'�' },
	{ "....",'�' },
	{ "-.-.",'�' },
	{ "---.",'�' },
	{ "----",'�' },
	{ "--.-",'�' },
	{ "-.--",'�' },
	{ "-..-",'�' },
	{ "..-..",'�' },
	{ "..--",'�' },
	{ ".-.-",'�' },
	{ ".----",'1' },
	{ "..---",'2' },
	{ "...--",'3' },
	{ "....-",'4' },
	{ ".....",'5' },
	{ "-....",'6' },
	{ "--...",'7' },
	{ "---..",'8' },
	{ "----.",'9' },
	{ "-----",'0' },
	{ "......",'.' },
	{ ".-.-.-",',' },
	{ "---...",':' },
	{ "-.-.-.",';' },
	{ "-.--.-",'(' },
	{ "-.--.-",')' },
	{ ".-..-.",'"' },
	{ "-....-",'-' },
	{ "-..-.",'/' },
	{ "..--..",'?' },
	{ "--..--",'!' },
	{ "-...-",' ' },
	{ ".--.-.",'@' },
};
map <string, unsigned char> CorrectMap = {
	{"�", ' ' },
	{ "�", ' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�", ' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�", ' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "�",' ' },
	{ "1",' ' },
	{ "2",' ' },
	{ "3",' ' },
	{ "4",' ' },
	{ "5",' ' },
	{ "6",' ' },
	{ "7",' ' },
	{ "8",' ' },
	{ "9",' ' },
	{ "0",' ' },
	{ ".",' ' },
	{ ",",' ' },
	{ ":",' ' },
	{ ";",' ' },
	{ "(",' ' },
	{ ")",' ' },
	{ "-",' ' },
	{ "/",' ' },
	{ "?",' ' },
	{ "!",' ' },
	{ "@",' ' },
};
class Morze {

public:
	static void TextToMorze(string text) {
		int buffer_counter = 0;
		int counter = 0;
		string final_string;
		int i = 0;
		for (int i = 0; i < text.length(); i++) {
			if (text[i] != ' ') {
				final_string += text[i];
			}
			else if (text[i] == ' ' || text.length() - i == 1) {
				auto Value = morzeMap.find(final_string);
				if (Value != morzeMap.end())
					cout << Value->second;
				final_string = "";

			}

		}

	}
	static string CorrectText(string text) 
	{
		string result;
		auto Value = CorrectMap.find(text);
		if (Value != morzeMap.end()) {
			string buf = " ";
			result.append(buf);
		}
		return result;
	}
	static bool CheckIfValid(string text) {
		int counter_spaces;
		
		for (int i = 0; i < text.length(); i++) {
			if (text[i] == '-' || text[i] == '.' || text[i] == ' ') {
				if (text[i] == ' ' && text[i + 1] == ' ') return false;
			}
			if (text[i] != '-' && text[i] != '.' && text[i] != ' ') return false;
		}
		return true;
	}
};

int main() {
	SetConsoleCP(1251);
	SetConsoleOutputCP(1251);
	string text;
	string buf;
	int choise;
	bool exit = false;
	while (!exit) {
		system("cls");
		cout << "�������� ��������:\n 1 - ����������� ������ �� �����\n 2-�������� ���� � ������ �����(����������)\n 3-�������� ���� � ����� �����" << endl;
		cin >> choise;
		switch (choise)
		{
		case 1: {
			ifstream ifs;
			ifs.open("input.txt");
			getline(ifs, text, '\0');
			ifs.close();
			cout << "�������� ������: " << text << endl << "���������:\t";
			if (Morze::CheckIfValid(text)) {
				if (text[text.length() - 1] != ' ') text.append(" ");
				Morze::TextToMorze(text);
			}
			else cout << "������ �����������";
			cout << endl;
			Morze::CorrectText(text);
			break;
		}
		case 2: {
			ofstream into_fileV2;
			into_fileV2.open("input.txt");
			cout << "������� ������ � ���� ������ ����� ��� ������ � ����:" << endl;
			cin >> text;
			getline(cin, buf);
			text.append(buf);
			if (Morze::CheckIfValid(text))
			{
				into_fileV2 << text;
				into_fileV2.close();
			}
			else cout << "���� ������ �����������";
			into_fileV2.close();
			break;
		}
		case 3: {
			ofstream into_fileV3;
			string bufv2 = " -...- ";
			into_fileV3.open("input.txt", std::ios::app);
			cout << "������� ������ � ���� ������ ����� ��� ������ � ����:" << endl;
			cin >> text;
			getline(cin, buf);
			text.append(buf);
			bufv2.append(text);
			if (Morze::CheckIfValid(bufv2))
			{
				into_fileV3 << bufv2;
				into_fileV3.close();
			}
			else cout << "���� ������ �����������";
			into_fileV3.close();
			break;
		}
		}
		cout << "\n��� ��������?? 1-��/2-���\n";
		cin >> choise;
		if (choise != 1) exit = true;
	}
	system("pause");
	return 0;
}



