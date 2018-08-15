#include <iostream>
#include <cstdlib>
#include <cstdio>
#include <ctime>
using namespace std;

#define MAX_SIZE 100

int N, B;

void clearMines(int size, char board[MAX_SIZE][MAX_SIZE], int x, int y);
// recursive function to clear mines at the point

void drawBoard(char board[MAX_SIZE][MAX_SIZE], bool done);
// drawing board with color

int main(int argc, char const *argv[])
{
	bool verified;

	verified = false;
	do {
		cout << "Ukuran board :"; cin >> N;
		if (N < 0 || N > MAX_SIZE) {
			cout << "Number invalid. Please insert a number between 1 - " << MAX_SIZE << endl;
		}
		else {
			verified = true;
		}
	} while (!verified);

	verified = false;
	do {
		cout << "Banyak mines :"; cin >> B;
		if (B < 0 || B > (N*N-1)) {
			cout << "Number invalid. Please insert a number between 1 - " << N*N-1 << endl;
		}
		else {
			verified = true;
		}
	} while (!verified);

	char board[MAX_SIZE][MAX_SIZE];
	
	/*
		# # #
		# @ #
		# # #
		Board Rule
		# = untouch
		@ = mark as mine
		! = mines
		0 = no mines around
		1 = 1 mines around
		2 = 2 mines around
		...
		8 = 8 mines around
	*/

	cout << endl << "Preparing the game..." << endl;
	// clearing from mines
	for (int i = 0; i < N; ++i)
	{
		/* code */
		for (int j = 0; j < N; ++j)
		{
			/* code */
			board[i][j] = '#';
		}
	}

	// adding mines
	srand( time( NULL ) );
	int mines = 0;
	int tempX, tempY;
	while (mines < B) {
		tempX = rand() % N;
		tempY = rand() % N;
		if (board[tempX][tempY] == '#') {
			board[tempX][tempY] = '!';
			mines++;
		}
	}
 
	// Starting the game
	cout << "Game start" << endl;
	cout << "(0,0) is on top-left of the board" << endl;
	cout << "Enjoy the Game!" << endl;
	bool win = true, finish = false, click;
	while (!finish) {
		// Drawing board
		drawBoard(board, finish);

		// taking command
		bool sure = false;
		while (!sure) {
			verified = false;
			do {
				cout << "Input X :"; cin >> tempX;
				if (tempX < 0 || tempX >= N) {
					cout << "Number invalid. Please insert a number between 0 - " << N-1 << endl;
				}
				else {
					verified = true;
				}
			} while (!verified);
			verified = false;
			do {
				cout << "Input Y :"; cin >> tempY;
				if (tempY < 0 || tempY >= N) {
					cout << "Number invalid. Please insert a number between 0 - " << N-1 << endl;
				}
				else {
					verified = true;
				}
			} while (!verified);
			
			cout << "rightClick (1) or leftClick(0) :"; cin >> click;
			cout << endl;
			cout << "Command confirmation" << endl;
			if (click) {
				cout << "Opening at coordinate (" << tempX << "," << tempY << ")" << endl;
			}
			else {
				cout << "Mine-sign at coordinate (" << tempX << "," << tempY << ")" << endl;
			}
			cout << "confirm (1) or cancel(0) :"; cin >> sure;
		}	

		cout << endl;

		// processing command
		if (click) {	
			if (board[tempY][tempX] == '!') {
				win = false;
				finish = true;
			}
			else if (board[tempY][tempX] == '@') {
				// do nothing
			}
			else {
				clearMines(N, board, tempY, tempX);
				// Checking if the game has finished
				finish = true;
				for (int i = 0; i < N; ++i)
				{
					/* code */
					for (int j = 0; j < N; ++j)
					{
						/* code */
						if (board[i][j] == '#') {
							finish = false;
							break;
						}	
						
					}
				}
			}	
		}
		else {
			if (board[tempY][tempX] == '@') {
				board[tempY][tempX] = '#';
				mines++;
			}
			else {
				if (mines == 0) {
					cout << "\033[1;31m" << "All mines has been signed" << "\033[0m" << endl;
				}
				else {
					board[tempY][tempX] = '@';	
					mines--;	
				}
			}
		}	
	}

	if (win) {
		cout << endl << "You Solve It!" << endl;
	}
	else {
		cout << endl << "You Hit the Bomb Dudee" << endl;
	}

	drawBoard(board, finish);
	
	return 0;
}

void clearMines( int size, char board[MAX_SIZE][MAX_SIZE], int x, int y)
// recursive function to clear mines at the point
{
	if (board[x][y] == '#') {
		int count = 0;

		// checking 8 direction
		// North
		if (board[x-1][y] == '!') {
			count++;
		}
		// North east
		if (board[x-1][y + 1] == '!') {
			count++;
		}
		// East
		if (board[x][y + 1] == '!') {
			count++;
		}
		// South East
		if (board[x+1][y + 1] == '!') {
			count++;
		}
		// South
		if (board[x+1][y] == '!') {
			count++;
		}
		// South West
		if (board[x+1][y - 1] == '!') {
			count++;
		}
		// West
		if (board[x][y - 1] == '!') {
			count++;
		}
		// North West
		if (board[x-1][y - 1] == '!') {
			count++;
		}

		board[x][y] = '0' + count;
		if (count == 0) {
			// checking other direction
			clearMines(size, board, x, y+1);
			clearMines(size, board, x-1, y+1);
			clearMines(size, board, x+1, y+1);
			clearMines(size, board, x-1, y);
			clearMines(size, board, x+1, y);
			clearMines(size, board, x+1, y-1);
			clearMines(size, board, x, y-1);
			clearMines(size, board, x-1, y-1);
		}
	}
}

void drawBoard(char board[MAX_SIZE][MAX_SIZE], bool done)
// drawing board with color
{
	cout << "  ";
	for (int i = 0; i < N; ++i)
	{
		/* code */
		cout << "\033[0;35m" << i % 10<< "\033[0m" << " ";
		// cout << i % 10 << " ";
	}
	cout << endl;
	for (int i = 0; i < N; ++i)
	{
		/* code */
		cout << "\033[0;35m" << i % 10<< "\033[0m" << " ";
		for (int j = 0; j < N; ++j)
		{
			/* code */
			switch (board[i][j]) {
				case '#' :
					cout << "\033[1;37m" << board[i][j] << "\033[0m" << " ";		
					break;
				case '@' :
					cout << "\033[1;31m" << board[i][j] << "\033[0m" << " ";		
					break;
				case '!' :
					if (done) {
						cout << "\033[0;31m" << '!' << "\033[0m" << " ";
					}
					else {
						cout << "\033[1;37m" << '#' << "\033[0m" << " ";
					}		
					break;
				case '0' :
					cout << "\033[0;37m" << board[i][j] << "\033[0m" << " ";		
					break;
				case '1' :
					cout << "\033[0;32m" << board[i][j] << "\033[0m" << " ";		
					break;
				case '2' :
					cout << "\033[0;33m" << board[i][j] << "\033[0m" << " ";		
					break; 
				case '3' :
					cout << "\033[0;34m" << board[i][j] << "\033[0m" << " ";		
					break;
				case '4' :
					cout << "\033[0;36m" << board[i][j] << "\033[0m" << " ";		
					break;
				case '5' :
					cout << "\033[0;32m" << board[i][j] << "\033[0m" << " ";		
					break;
				case '6' :
					cout << "\033[0;33m" << board[i][j] << "\033[0m" << " ";		
					break;
				case '7' :
					cout << "\033[0;34m" << board[i][j] << "\033[0m" << " ";		
					break;
				case '8' :
					cout << "\033[0;36m" << board[i][j] << "\033[0m" << " ";		
					break; 
			}
			
			
		}
		cout << endl;
	}
}