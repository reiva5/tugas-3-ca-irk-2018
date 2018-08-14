#include <iostream>
#include <cstdlib>
using namespace std;

int** reveal(int**,int, int, int);

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
	int** M = 0;
	M = new int*[N];
	
	for (int i=0; i<N; i++) {
		M[i] = new int[N];
		
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
	
	for (int i=0; i<N; i++) {
		for (int j=0; j<N; j++) {
			if (M[i][j] == 9) {
				if ((i>0) && (i<N-1)) {
					if ((j>0) && (j<N-1)) {
						if (M[i-1][j-1] != 9) {
							M[i-1][j-1] += 1;
						}
						if (M[i-1][j] != 9) {
							M[i-1][j] += 1;
						}
						if (M[i-1][j+1] != 9) {
							M[i-1][j+1] += 1;
						}
						if (M[i][j-1] != 9) {
							M[i][j-1] += 1;
						}
						if (M[i][j+1] != 9) {
							M[i][j+1] += 1;
						}
						if (M[i+1][j-1] != 9) {
							M[i+1][j-1] += 1;
						}
						if (M[i+1][j] != 9) {
							M[i+1][j] += 1;
						}
						if (M[i+1][j+1] != 9) {
							M[i+1][j+1] += 1;
						}
					} else if (j == 0) {
						if (M[i-1][j] != 9) {
							M[i-1][j] += 1;
						}
						if (M[i-1][j+1] != 9) {
							M[i-1][j+1] += 1;
						}
						if (M[i][j+1] != 9) {
							M[i][j+1] += 1;
						}
						if (M[i+1][j] != 9) {
							M[i+1][j] += 1;
						}
						if (M[i+1][j+1] != 9) {
							M[i+1][j+1] += 1;
						}
					} else if (j==N-1) {
						if (M[i-1][j-1] != 9) {
							M[i-1][j-1] += 1;
						}
						if (M[i-1][j] != 9) {
							M[i-1][j] += 1;
						}
						if (M[i][j-1] != 9) {
							M[i][j-1] += 1;
						}
						if (M[i+1][j-1] != 9) {
							M[i+1][j-1] += 1;
						}
						if (M[i+1][j] != 9) {
							M[i+1][j] += 1;
						}
					}
				} else if (i==0) {
					if ((j>0) && (j<N-1)) {
						if (M[i][j-1] != 9) {
							M[i][j-1] += 1;
						}
						if (M[i][j+1] != 9) {
							M[i][j+1] += 1;
						}
						if (M[i+1][j-1] != 9) {
							M[i+1][j-1] += 1;
						}
						if (M[i+1][j] != 9) {
							M[i+1][j] += 1;
						}
						if (M[i+1][j+1] != 9) {
							M[i+1][j+1] += 1;
						}
					} else if (j == 0) {
						if (M[i][j+1] != 9) {
							M[i][j+1] += 1;
						}
						if (M[i+1][j] != 9) {
							M[i+1][j] += 1;
						}
						if (M[i+1][j+1] != 9) {
							M[i+1][j+1] += 1;
						}
					} else if (j==N-1) {
						if (M[i][j-1] != 9) {
							M[i][j-1] += 1;
						}
						if (M[i+1][j-1] != 9) {
							M[i+1][j-1] += 1;
						}
						if (M[i+1][j] != 9) {
							M[i+1][j] += 1;
						}
					}
				} else if (i==N-1) {
					if ((j>0) && (j<N-1)) {
						if (M[i-1][j-1] != 9) {
							M[i-1][j-1] += 1;
						}
						if (M[i-1][j] != 9) {
							M[i-1][j] += 1;
						}
						if (M[i-1][j+1] != 9) {
							M[i-1][j+1] += 1;
						}
						if (M[i][j-1] != 9) {
							M[i][j-1] += 1;
						}
						if (M[i][j+1] != 9) {
							M[i][j+1] += 1;
						}
					} else if (j == 0) {
						if (M[i-1][j] != 9) {
							M[i-1][j] += 1;
						}
						if (M[i-1][j+1] != 9) {
							M[i-1][j+1] += 1;
						}
						if (M[i][j+1] != 9) {
							M[i][j+1] += 1;
						}
					} else if (j==N-1) {
						if (M[i-1][j-1] != 9) {
							M[i-1][j-1] += 1;
						}
						if (M[i-1][j] != 9) {
							M[i-1][j] += 1;
						}
						if (M[i][j-1] != 9) {
							M[i][j-1] += 1;
						}
					}
				}
			}
		}
	}
	
	cout << endl << "-- INFORMATION --" << endl;
	cout << "! : Bomb" << endl;
	cout << "- : Opened area" << endl;
	cout << "# : Unopened area" << endl;
	cout << "1-8 : Number of bomb(s) around the area" << endl << endl; 
	
	// Play game
	int end = 0;
	while (!end) {
		for (int i=-1; i<N; i++) {
			for (int j=-1; j<N; j++) {
				if (i == -1) {
					if (j == -1) {
						cout << "   ";
					} else if (j == N-1) {
						cout << j+1 << endl;
					} else {
						cout << j+1;
					}
				} else {
					if (j == -1) {
						cout << i+1 << "  ";
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
		
		if (end != 2) {
			M = reveal(M,x,y,N);
		}
		
		int count = 0;
		for (int i=0; i<N; i++) {
			for (int j=0; j<N; j++) {
				if ((M[i][j] >= 0) && (M[i][j] <= 10)) {
					count++;
				}
			}
		}
		if (count == B) {
			end = 1;
		}
		cout << endl;
	}
	
	// End Game
	
	for (int i=-1; i<N; i++) {
		for (int j=-1; j<N; j++) {
			if (i == -1) {
				if (j == -1) {
					cout << "   ";
				} else if (j == N-1) {
					cout << j+1 << endl;
				} else {
					cout << j+1;
				}
			} else {
				if (j == -1) {
					cout << i+1 << "  ";;
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
		cout << endl << "== SORRY.. YOU LOSE ==" << endl;
	} else {
		cout << endl << "== CONGRATULATION!! YOU WIN ==" << endl;
	}
	
	return 0;
}

int** reveal(int**M, int x, int y, int N) {
	int** temp = 0;
	temp = new int*[N];
	
	for (int i=0; i<N; i++) {
		temp[i] = new int[N];
		
		for (int j=0; j<N; j++) {
			temp[i][j] = M[i][j];
		}
	}
	
	if ((x>0) && (x<N-1)) {
		if ((y>0) && (y<N-1)) {
			if (M[x][y] == 0) {
				temp[x][y] = -1;
				temp = reveal(temp, x-1, y-1, N);
				temp = reveal(temp, x-1, y, N);
				temp = reveal(temp, x-1, y+1, N);
				temp = reveal(temp, x, y+1, N);
				temp = reveal(temp, x, y-1, N);
				temp = reveal(temp, x+1, y-1, N);
				temp = reveal(temp, x+1, y, N);
				temp = reveal(temp, x+1, y+1, N);
			} else if ((M[x][y] < 9) && (M[x][y] > 0)) {
				temp[x][y] += 10;
			}
		} else if (y==0) {
			if (M[x][y] == 0) {
				temp[x][y] = -1;
				temp = reveal(temp, x-1, y, N);
				temp = reveal(temp, x-1, y+1, N);
				temp = reveal(temp, x, y+1, N);
				temp = reveal(temp, x+1, y, N);
				temp = reveal(temp, x+1, y+1, N);
			} else if ((M[x][y] < 9) && (M[x][y] > 0)) {
				temp[x][y] += 10;
			}
		} else if (y==N-1) {
			if (M[x][y] == 0) {
				temp[x][y] = -1;
				temp = reveal(temp, x-1, y, N);
				temp = reveal(temp, x-1, y-1, N);
				temp = reveal(temp, x, y-1, N);
				temp = reveal(temp, x+1, y, N);
				temp = reveal(temp, x+1, y-1, N);
			} else if ((M[x][y] < 9) && (M[x][y] > 0)) {
				temp[x][y] += 10;
			}
		}
	} else if (x==0) {
		if ((y>0) && (y<N-1)) {
			if (M[x][y] == 0) {
				temp[x][y] = -1;
				temp = reveal(temp, x, y+1, N);
				temp = reveal(temp, x, y-1, N);
				temp = reveal(temp, x+1, y-1, N);
				temp = reveal(temp, x+1, y, N);
				temp = reveal(temp, x+1, y+1, N);
			} else if ((M[x][y] < 9) && (M[x][y] > 0)) {
				temp[x][y] += 10;
			}
		} else if (y==0) {
			if (M[x][y] == 0) {
				temp[x][y] = -1;
				temp = reveal(temp, x, y+1, N);
				temp = reveal(temp, x+1, y, N);
				temp = reveal(temp, x+1, y+1, N);
			} else if ((M[x][y] < 9) && (M[x][y] > 0)) {
				temp[x][y] += 10;
			}
		} else if (y==N-1) {
			if (M[x][y] == 0) {
				temp[x][y] = -1;
				temp = reveal(temp, x, y-1, N);
				temp = reveal(temp, x+1, y, N);
				temp = reveal(temp, x+1, y-1, N);
			} else if ((M[x][y] < 9) && (M[x][y] > 0)) {
				temp[x][y] += 10;
			}
		}
	} else if (x==N-1) {
		if ((y>0) && (y<N-1)) {
			if (M[x][y] == 0) {
				temp[x][y] = -1;
				temp = reveal(temp, x-1, y-1, N);
				temp = reveal(temp, x-1, y, N);
				temp = reveal(temp, x-1, y+1, N);
				temp = reveal(temp, x, y+1, N);
				temp = reveal(temp, x, y-1, N);
			} else if ((M[x][y] < 9) && (M[x][y] > 0)) {
				temp[x][y] += 10;
			}
		} else if (y==0) {
			if (M[x][y] == 0) {
				temp[x][y] = -1;
				temp = reveal(temp, x-1, y, N);
				temp = reveal(temp, x-1, y+1, N);
				temp = reveal(temp, x, y+1, N);
			} else if ((M[x][y] < 9) && (M[x][y] > 0)) {
				temp[x][y] += 10;
			}
		} else if (y==N-1) {
			if (M[x][y] == 0) {
				temp[x][y] = -1;
				temp = reveal(temp, x-1, y, N);
				temp = reveal(temp, x-1, y-1, N);
				temp = reveal(temp, x, y-1, N);
			} else if ((M[x][y] < 9) && (M[x][y] > 0)) {
				temp[x][y] += 10;
			}
		}
	}
	return temp;
}
