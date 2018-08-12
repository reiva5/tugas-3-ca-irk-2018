#ifndef MINESWEEPER_H
#define MINESWEEPER_H

#include <iostream>
#include <string>
#include <stdio.h>
#include <windows.h>

using namespace std;
typedef unsigned char byte;
 
enum fieldValues : byte { 
    OPEN, 
    CLOSED = 10, 
    MINE
};
 
class fieldData {
public:
    fieldData() : value(CLOSED), open(false) {}
    byte value;
    bool open, mine;
};
 
class Minesweeper {
public:
    Minesweeper(int n, int mine);
    ~Minesweeper();

    void NewTurn(); //Untuk menggambar tabel permainan, dan menerima inputan koordinat pemain
    void OpenCell(int x, int y);    //Untuk membuka kotak yang dipilih pemain
    void DrawBoard();   //Untuk menggambar kotak permainan
    void CheckWin();    //Mengecek apakah sudah menang atau belum
    void Explode();    //Aksi yang terjadi ketika memilih kotak berisi bom
    void PrintMessage(string s);    //Menulis pesan ketika akhir permainan
    void OpenTile(int x, int y);    //Membuka kotak secara rekursif
    int CountMine(int x, int y);    //Menghitung bom yang berada di sekitar koordinat (x,y)

    bool isInside(int x, int y);    //Mengecek apakah koordinat (x,y) berada di dalam kotak permainan

private:
    int size, oMines;
    int graphs[3];
    fieldData* field;
    bool end;
};

#endif
