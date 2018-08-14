#include <iostream>
#include <cstdlib>
using namespace std;

int main () {
	cout << "==== MINESWEEPER ====" << endl;
	cout << "By : Kurniandha Sukma Yunastrian - 13516106" << endl << endl;
	cout << "Input size of board (NxN) from 1 to 100 : ";
	
	// Input board size
	int N;	
	do {
		cin >> N;
		if ((N < 1) || (N > 100)) {
			cout << endl << "Invalid input!" << endl;
			cout << "Please input from 1 to 100" << endl;
			cout << "Input size of board (NxN) from 1-100 : ";
		}
	} while ((N < 1) || (N > 100));
	
	// Init board
	char M[N][N];
	for (int i=0; i<N; i++) {
		for (int j=0; j<N; j++) {
			M[i][j] = '#';
		}
	}
	
	// Init bomb
	int B;
	cout << "Input the number of bomb from 1 to " << N*N << " : ";
	cin >> B;
	int bombLocationX[B];
	int bombLocationY[B];
	for (int i=0; i<B; i++) {
		int x = rand() % N;
		int y = rand() % N;
		if (M[x][y] == '#') {
			M[x][y] = '!';
			bombLocationX[i] = x;
			bombLocationY[i] = y;
		} else {
			i--;
		}
	}
	
	for (int i=0; i<B; i++) {
		M[bombLocationX[i]][bombLocationY[i]] = '#';
	}
	
	// Play game
	int end = 0;
	while (!end) {
		for (int i=-1; i<N; i++) {
			for (int j=-1; j<N; j++) {
				if (i == -1) {
					if (j == -1) {
						cout << " ";
					} else {
						cout << j+1;
					}
				} else {
					if (j == -1) {
						cout << i+1;
					} else {
						cout << M[i][j];	
					}	
				}
			}
			cout << endl;
		}
		
		cout << "Input the cell coordinate \'row column\' : ";
		int x,y;
		cin >> x >> y;
		x--;
		y--;
		for (int i=0; i<B; i++) {
			if ((x == bombLocationX[i]) && (y == bombLocationY[i])) {
				end = 2;
			}
		}
		
		if (end != 2) {
			bool open = true;
			while (open) {
				
			}
		}
		cout << endl;
	}
	
	// End Game
	
	for (int i=0; i<B; i++) {
		M[bombLocationX[i]][bombLocationY[i]] = '!';
		cout << bombLocationX[i] << " " << bombLocationY[i] << endl;
	}
	
	for (int i=-1; i<N; i++) {
		for (int j=-1; j<N; j++) {
			if (i == -1) {
				if (j == -1) {
					cout << " ";
				} else {
					cout << j+1;
				}
			} else {
				if (j == -1) {
					cout << i+1;
				} else {
					cout << M[i][j];	
				}	
			}
		}
		cout << endl;
	}
	
	if (end == 2) {
		cout << "YOU LOSE" << endl;
	} else {
		cout << "YOU WIN" << endl;
	}
	
	return 0;
}
