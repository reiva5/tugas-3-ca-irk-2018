 
#include<bits/stdc++.h>
using namespace std;

#define MAXSIDE 25
#define MAXMINES 99
#define MOVESIZE 526 // (25 * 25 - 99)

int SIDE ; // side length of the board
int MINES ; // number of mines on the board
 
// A Utility Function to check whether given cell (row, col)
// is a valid cell or not
bool isValid(int row, int col)
{
    // Returns true if row number and column number
    // is in range
    return (row >= 0) && (row < SIDE) &&
           (col >= 0) && (col < SIDE);
}
 
// A Utility Function to check whether given cell (row, col)
// has a mine or not.
bool isMine (int row, int col, char board[][MAXSIDE])
{
    return (board[row][col] == '*');
}
 
// A Function to get the user's move
void makeMove(int *x, int *y)
{
    // Take the input move
    printf(">> Enter your move, (row, column) -> ");
    scanf("%d %d", x, y);
}
 
// A Function to print the current gameplay board
void printBoard(char myBoard[][MAXSIDE])
{
    int i, j;
 
    printf ("    ");
 
    for (i=0; i<SIDE; i++)
        printf ("%d ", i);
 
    printf ("\n\n");
 
    for (i=0; i<SIDE; i++)
    {
        printf ("%d   ", i);
 
        for (j=0; j<SIDE; j++)
            printf ("%c ", myBoard[i][j]);
        printf ("\n");
    }
    return;
}
 
// A Function to count the number of
// mines in the adjacent cells
int countAdjacentMines(int row, int col, int mines[][2], char realBoard[][MAXSIDE]){
 
    int i;
    int count = 0;
 
    if (isValid (row-1, col)){
        if (isMine (row-1, col, realBoard))
            count++;
    }
 
    if (isValid (row+1, col)){
        if (isMine (row+1, col, realBoard))
            count++;
    }
 
    if (isValid (row, col+1)){
        if (isMine (row, col+1, realBoard))
            count++;
    }
 
    if (isValid (row, col-1)){
        if (isMine (row, col-1, realBoard))
            count++;
    }
 
    if (isValid (row-1, col+1)){
        if (isMine (row-1, col+1, realBoard))
            count++;
    }
 
    if (isValid (row-1, col-1)){
        if (isMine (row-1, col-1, realBoard))
            count++;
    }
 
    if (isValid (row+1, col+1)){
        if (isMine (row+1, col+1, realBoard))
            count++;
    }
 
    if (isValid (row+1, col-1)){
        if (isMine (row+1, col-1, realBoard))
            count++;
    }
 
    return count;
}
 
//A Recursive Fucntion to play the Minesweeper Game
bool continuePlay(char myBoard[][MAXSIDE], char realBoard[][MAXSIDE], int mines[][2], int row, int col, int *movesLeft){
 
    // Base Case
    if (myBoard[row][col] != '-'){
        return false;
    }
 
    int i, j;
 
    // a mine opened
    // going to lose
    if (realBoard[row][col] == '*'){
        myBoard[row][col]='*';
 
        for (i=0; i<MINES; i++)
            myBoard[mines[i][0]][mines[i][1]]='*';
 
        printBoard (myBoard);
        printf ("\nYou lost!\n");
        return true;
    }
 
    else
     {
        // Calculate the number of adjacent mines and put it
        // on the board
        int count = countAdjacentMines(row, col, mines, realBoard);
        (*movesLeft)--;
 
        myBoard[row][col] = count + '0';
 
        if (count==0){
            if (isValid (row-1, col)){
                if (!isMine (row-1, col, realBoard))
                    continuePlay(myBoard, realBoard, mines, row-1, col, movesLeft);
            }
 
            if (isValid (row+1, col)){
                if (!isMine (row+1, col, realBoard))
                    continuePlay(myBoard, realBoard, mines, row+1, col, movesLeft);
            }
 
            if (isValid (row, col+1)){
                if (!isMine (row, col+1, realBoard))
                    continuePlay(myBoard, realBoard, mines, row, col+1, movesLeft);
            }
 
            if (isValid (row, col-1)){
                   if (!isMine (row, col-1, realBoard))
                    continuePlay(myBoard, realBoard, mines, row, col-1, movesLeft);
            }
 
            if (isValid (row-1, col+1)){
                if (!isMine (row-1, col+1, realBoard))
                    continuePlay(myBoard, realBoard, mines, row-1, col+1, movesLeft);
            }
 
            if (isValid (row-1, col-1)){
                 if (!isMine (row-1, col-1, realBoard))
                    continuePlay(myBoard, realBoard, mines, row-1, col-1, movesLeft);
            }
 
            if (isValid (row+1, col+1)){
                 if (!isMine (row+1, col+1, realBoard))
                    continuePlay(myBoard, realBoard, mines, row+1, col+1, movesLeft);
            }

            if (isValid (row+1, col-1)){
                if (!isMine (row+1, col-1, realBoard))
                    continuePlay(myBoard, realBoard, mines, row+1, col-1, movesLeft);
            }
        }
 
        return false;
    }
}
 
// A Function to place the mines randomly
// on the board
void placeMines(int mines[][2], char realBoard[][MAXSIDE]){
    bool mem[MAXSIDE*MAXSIDE];
 
    memset (mem, false, sizeof (mem));
 
    // Continue until all random mines have been created.
    for (int i=0; i<MINES; )
     {
        int random = rand() % (SIDE*SIDE);
        int x = random / SIDE;
        int y = random % SIDE;
 
        // Add the mine if no mine is placed at this
        // position on the board
        if (mem[random] == false)
        {
            // Row Index of the Mine
            mines[i][0]= x;
            // Column Index of the Mine
            mines[i][1] = y;
 
            // Place the mine
            realBoard[mines[i][0]][mines[i][1]] = '*';
            mem[random] = true;
            i++;
        }
    }
}
 
// A Function to initialise the game
void initialise(char realBoard[][MAXSIDE], char myBoard[][MAXSIDE]){
    // Initiate the random number generator so that
    // the same configuration doesn't arises
    srand(time (NULL));
 
    // Assign all the cells as mine-free
    for (int i=0; i<SIDE; i++)
    {
        for (int j=0; j<SIDE; j++)
        {
            myBoard[i][j] = realBoard[i][j] = '-';
        }
    }
}
 
// A Function to cheat by revealing where the mines are
// placed.
void cheatMinesweeper (char realBoard[][MAXSIDE]){
    printf ("The mines locations are-\n");
    printBoard (realBoard);
    return;
}
 
// A function to replace the mine from (row, col) and put
// it to a vacant space
void replaceMine (int row, int col, char board[][MAXSIDE]){
    for (int i=0; i<SIDE; i++){
        for (int j=0; j<SIDE; j++){
            if (board[i][j] != '*'){
                board[i][j] = '*';
                board[row][col] = '-';
                return;
            }
        }
    }
}
 
// A Function to play Minesweeper game
void playMinesweeper (){
    // Initially the game is not over
    bool gameOver = false;
 
    // Actual Board and My Board
    char realBoard[MAXSIDE][MAXSIDE], myBoard[MAXSIDE][MAXSIDE];
 
    int movesLeft = SIDE * SIDE - MINES;
    int x, y;
    int mines[MAXMINES][2]; // stores (x,y) coordinates of all mines.
 
    initialise (realBoard, myBoard);
 
    // Place the Mines randomly
    placeMines (mines, realBoard);
 
     /*
     If you want to cheat and know
     where mines are before playing the game
     then uncomment this part
    */
    //cheatMinesweeper(realBoard);
 
    // You are in the game until you have not opened a mine
    // So keep playing
 
    int currentMoveIndex = 0;
    while (gameOver == false){
        printf ("Current Status of Board : \n");
        printBoard (myBoard);
        makeMove (&x, &y);
 
        if (currentMoveIndex == 0){
            // If the first move itself is a mine
            // then we remove the mine from that location
            if (isMine (x, y, realBoard))
                replaceMine (x, y, realBoard);
        }
 
        currentMoveIndex ++;
 
        gameOver = continuePlay (myBoard, realBoard, mines, x, y, &movesLeft);
 
        if ((!gameOver) && (movesLeft == 0)){
            printf ("\nYou won !\n");
            gameOver = true;
        }
    }
}
 
// A Function to choose the difficulty level
// of the game
void startGame (){
    int N, B;
    bool valid = false;                                                                                                                   

    printf("             . . .\n");                         
    printf("              \\|/\n");                          
    printf("            `--+--'\n");                        
    printf("              /|\\\n");                          
    printf("             ' | '\n");                         
    printf("               |\n");                           
    printf("               |\n");                           
    printf("           ,--'#`--.\n");                       
    printf("           |#######|\n");                       
    printf("        _.-'#######`-._\n");                    
    printf("     ,-'###############`-.\n");                 
    printf("   ,'#####################`,\n");               
    printf("  /#########################\\\n");              
    printf(" |###########################|\n");             
    printf("|#############################|\n");            
    printf("|#############################|\n");            
    printf("|#############################|\n");            
    printf("|#############################|\n");            
    printf(" |###########################|\n");             
    printf("  \\#########################/\n");              
    printf("   `.#####################,'\n");               
    printf("     `._###############_,'\n");                 
    printf("        `--..#####..--'\n");
    printf("\n");
    printf("             BOOM !\n");
    printf("\n");
    printf ("--- Welcome to Minesweeper Console ---\n");
    printf ("      brought to you by 13516081      \n");
    
    while(!valid){
        printf (">> Enter the board size -> ");
        scanf ("%d", &N);
        printf (">> Enter the number of Mines -> ");
        scanf ("%d", &B);
        if(B <= N*N){
            valid = true;
        }else{
            printf(">> Your input is invalid\n");
        }
    }
    SIDE = N;
    MINES = B;
}
 
// Driver Program to test above functions
int main(){
    startGame();
 
    playMinesweeper ();
 
    return 0;
}