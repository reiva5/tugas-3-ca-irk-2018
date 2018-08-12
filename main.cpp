#include "Minesweeper.cpp"

int main() {
    int n, m;
    srand(GetTickCount());
    cout << "Ukuran board : ";
    cin >> n;
    cout << "Banyak mine : ";
    cin>> m;
    Minesweeper ms(n, m); 
    ms.NewTurn();
    return system("pause");
}