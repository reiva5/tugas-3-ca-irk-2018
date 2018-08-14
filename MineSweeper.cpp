#include <iostream>
#include <cstdlib>
using namespace std;

char** reveal(char**,int, int, int, int*, int*, int);

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
	int M[N][N];
	for (int i=0; i<N; i++) {
		for (int j=0; j<N; j++) {
			M[i][j] = 0;
		}
	}
	
	// Init bomb
	int B;
	cout << "Input the number of bomb from 1 to " << N*N << " : ";
	cin >> B;
	for (int i=0; i<B; i++) {
		int x = rand() % N;
		int y = rand() % N;
		if (M[x][y] == 0) {
			M[x][y] = 9;
		} else {
			i--;
		}
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
						if (M[i][j] > 10) {
							cout << M[i][j] - 10;
						} else if (M[i][j] == -1) {
							cout << '-';
						} else {
							cout << '#';
						}
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
		if (M[x][y] == 9) {
			end = 2;
		}
		
//		if (end != 2) {
//			bool open = true;
//			while (open) {
//				
//			}
//		}
		cout << endl;
	}
	
	// End Game
	
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
					if (M[i][j] > 10) {
						cout << M[i][j] - 10;
					} else if (M[i][j] == -1) {
						cout << '-';
					} else if (M[i][j] == 9) {
						cout << '!';
					} else {
						cout << '#';
					}
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

char** reveal(char**M, int x, int y, int N, int * BX, int * BY, int B) {
	if ((x>0) && (x<N-1)) {
		if ((y>0) && (y<N-1)) {
			
		}
	} 
}
