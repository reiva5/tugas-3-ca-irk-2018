#include <iostream>
#include <cstdlib>
using namespace std;

int main () {
	cout << "== MINESWEEPER ==" << endl;
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
	int bombLocationX[B];
	int bombLocationY[B];
	cout << "Input the number of bomb from 1 to " << N*N << " : ";
	cin >> B;
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
		for (int i=0; i<N; i++) {
			for (int j=0; j<N; j++) {
				cout << M[i][j];
			}
			cout << endl;
		}
		
		cout << "Input the cell coordinate \'row column\' : ";
		int x,y;
		cin >> x >> y;
	}
	
	return 0;
}
