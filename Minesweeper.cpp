#include "Minesweeper.h"

Minesweeper::Minesweeper(int n, int mine) {
    end = false;
    size = n;
    field = new fieldData[n * n];
    memset(field, 0, n * n * sizeof(fieldData));
    oMines = mine;
    int mx, my;
    for(int m = 0; m < mine; m++){
        do { 
            mx = rand() % n; 
            my = rand() % n; 
        } while (field[mx + size * my].mine);
        field[mx + size * my].mine = true;
    }
    graphs[0] = ' '; 
    graphs[1] = '.'; 
    graphs[2] = '*';
}

Minesweeper::~Minesweeper() { 
    if(field) {
        delete [] field;
    } 
}

void Minesweeper::NewTurn() {
    //string c, r;
    int col, row;
    while(!end) {
        DrawBoard();
        cout << "Koordinat (x y): ";
        cin >> col >> row;
        OpenCell(col-1, row-1);
    }
}

void Minesweeper::OpenCell(int x, int y) {
    //Bila koordinat di luar kotak permainan
    if(!isInside(x, y)) {
        cout << "Invalid koordinat!\n";
        Sleep(1000); 
        return;
    }
    
    fieldData* fd = &field[size * y + x];
    //Bila mencoba membuka kotak yang telah dibuka
    if(fd->open && fd->value < CLOSED) {
        cout << "Kotak ini telah dibuka!\n";
        Sleep(1000); 
        return;
    }

    //Bila membuka kotak yang berisi bom
    if(field[x + y * size].mine) {
        Explode();
    }
    else {
        OpenTile(x, y);
        CheckWin();
    }
}

void Minesweeper::DrawBoard() {
    int temp;

    cout << "\n";
    for(int i = 0; i < size; i++) {
        cout << "  " << i+1 << " "; 
    }
    cout << "\n";

    for(int i = 0; i < size; i++) {
        temp = i * size;
        for(int j = 0; j < size; j++) {
            cout << "----";
        }

        cout << "-\n"; 
        fieldData* fd;
        for(int j = 0; j < size; j++) {
            fd = &field[j + temp]; 
            cout<< "| ";
            if(!fd->open) {
                cout << (char)graphs[1] << " ";
            }
            else {
                if(fd->value > 9) {
                    cout << (char)graphs[fd->value - 9] << " ";
                }
                else {
                    if(fd->value < 1) {
                        cout << "  ";
                    }
                    else {
                        cout << (char)(fd->value + 48 ) << " ";
                    }
                }
            }
        }
        cout << "| " << i + 1 << "\n";
    }

    //Baris terakhir
    for( int j = 0; j < size; j++ ) {
        cout << "----";
    }
    cout << "-\n\n";
}

void Minesweeper::CheckWin() {
    int z = size * size - oMines, temp;
    fieldData* fd;
    for(int i = 0; i < size; i++) {
        temp = i * size;
        for(int j = 0; j < size; j++) {
            fd = &field[j + temp];
            if(fd->open) {
                z--;
            }
        }
    }

    if(z==0) {
        PrintMessage("Selamat, kamu berhasil menyelesaikan permainan ini!");
    }
}

void Minesweeper::Explode() {
    int temp; 
    fieldData* fd;
    for(int i = 0; i < size; i++) {
        temp = i * size;
        for(int j = 0; j < size; j++) {
            fd = &field[j + temp];
            if(fd->mine) {
                fd->open = true;
                fd->value = MINE;
            }
        }
    }
    PrintMessage("BOOMMM! Anda meledakkan bom \n\nGAME OVER");
}

void Minesweeper::PrintMessage(string s) {
    end = true; 
    DrawBoard();
    cout << s << "\n\n";
}

void Minesweeper::OpenTile(int x, int y) {
    if(!isInside(x, y) || field[x + y * size].open) {
        return;
    }
    
    int number = CountMine(x, y);

    field[x + y * size].open = true;
    field[x + y * size].value = number;
    
    if(number == 0) {
        for(int i = -1; i < 2; i++) {
            for(int j = -1; j < 2; j++) {
                if(i == 0 && j == 0) { //Jika merupakan kotak yang dibuka, maka tidak dibuka lagi
                    continue;
                }
                OpenTile(x + i, y + j);
            }
        }
    }
}

int Minesweeper::CountMine(int x, int y) {
    int m = 0;
    for(int i = -1; i < 2; i++){
        for(int j = -1; j < 2; j++) {
            if(i == 0 && j == 0) { //Jika merupakan kotak di koordinat (x,y), maka tidak dihitung
                continue;
            }
            if(isInside(x + i, y + j) && field[x + i + (y + j) * size].mine) { //Bila masih berada di kotak permainan, dan merupakan bom
                m++;
            }
        }
    }

    return m;
}

bool Minesweeper::isInside(int x, int y) { 
    return (x > -1 && y > -1 && x < size && y < size); 
}