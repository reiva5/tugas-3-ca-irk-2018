#include <iostream>
#include <cstdlib>
using namespace std;

int main () {
	cout << "== MINESWEEPER ==" << endl;
	cout << "By : Kurniandha Sukma Yunastrian - 13516106" << endl << endl;
	cout << "Input size of board (NxN) from 1-100 : ";
	
	int N;
		
	do {
		cin >> N;
		if ((N < 1) || (N > 100)) {
			cout << endl << "Invalid input!" << endl;
			cout << "Please input from 1-100" << endl;
			cout << "Input size of board (NxN) from 1-100 : ";
		}
	} while ((N < 1) || (N > 100));
	
	char M[N][N];
	for (int i=0; i<N; i++) {
		for (int j=0; j<N; j++) {
			M[i][j] = '#';
		}
	}
	
	for (int i=0; i<N; i++) {
		for (int j=0; j<N; j++) {
			cout << M[i][j];
		}
		cout << endl;
	}
	
	return 0;
}
