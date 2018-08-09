import random

N = -1
B = -1
board = []
finished = False
lose = False
# Constants
BOMB = -1


# Tile is the struct of a single tile in the board
class Tile:
    val = 0
    opened = False

# init_board initialize the game board
def init_board(N, B):
    global board
    board = [[Tile() for i in range(N+2)] for j in range(N+2)]

    bombs_planted = 0
    while (bombs_planted < B):
        randx, randy = random.randint(1, N), random.randint(1, N)
        if board[randx][randy].val != BOMB:
            board[randx][randy].val = BOMB
            bombs_planted += 1

    for i in range(1, N+1):
        for j in range(1, N+1):
            if board[i][j].val == BOMB:
                add_value(i-1, j+1)
                add_value(i, j+1)
                add_value(i+1, j+1)
                add_value(i-1, j)
                add_value(i+1, j)
                add_value(i-1, j-1)
                add_value(i, j-1)
                add_value(i+1, j-1)

    return


# add_value increase the counter of surrounding bombs of a tile
def add_value(x, y):
    global board
    if board[x][y].val != BOMB:
        board[x][y].val += 1
    return

# input_tile reads a user input and open it
def input_tile():
    x, y = input("Input Row , Column (eg. : '1 4' inputs the first row and the fourth column) : ").split()
    x, y = int(x), int(y)
    while x < 1 or y < 1 or x > N or y > N or board[x][y].opened == True:
        print('Invalid Row,Col')
        x, y = input("Input Row , Column (eg. : '1 4' inputs the first row and the fourth column) : ").split()
        x, y = int(x), int(y)
    return open_tile(x, y)

# open_tile opens a tile and stop the game if a Bomb is opened
def open_tile(x, y):
    global board
    if x < 1 or y < 1 or x > N or y > N or board[x][y].opened:
        return False
    if board[x][y].val == BOMB:
        return True

    board[x][y].opened = True

    if board[x][y].val == 0:
        open_tile(x-1, y-1)
        open_tile(x, y-1)
        open_tile(x+1, y-1)
        open_tile(x-1, y)
        open_tile(x+1, y)
        open_tile(x-1, y+1)
        open_tile(x, y+1)
        open_tile(x+1, y+1)

    return False

# print_board prints the game's board
def print_board():
    print()
    print('Remaining Bombs : ', B)
    print(end='   ')
    for i in range(1, N+1):
        print(i, end='  ')
        
    print()
    for i in range(1, N+1):
        print(i, end=' ')
        if i < 10:
            print(end=' ')
        for j in range(1, N+1):
            if not board[i][j].opened:
                print('#', end='  ')
            else:
                if board[i][j].val == BOMB:
                    print('X', end='  ')
                else:
                    print(board[i][j].val, end='  ')
        print()
    print()
    return

# print_end_game prints the true board which will be called at the end of the game
def print_end_game():
    global board
    for i in range(1, N+1):
        for j in range(1, N+1):
            board[i][j].opened = True
    print_board()
    return

# is_finished checks is the game has been won or not
def is_finished():
    tiles_opened = 0
    for i in range(1, N+1):
        for j in range(1, N+1):
            if board[i][j].opened:
                tiles_opened += 1
    return B == (N**2)-tiles_opened


if __name__ == "__main__":
    print('========== Minesweeper by Cornelius Yan M. - 13516113 ==========')
    N = int(input('Input N : '))
    B = int(input('Input number of Bombs : '))
    while B >= N**2 or B < 1:
        print('Input Bombs invalid ! Bombs must be around 0 < B < ', N**2)
        B = int(input('Input number of Bombs : '))

    init_board(N, B)

    while not finished:
        print_board()
        lose = input_tile()
        if not lose:
            finished = is_finished()
        else:
            finished = True

    print()
    if not lose:
        print('========== Game Over! You Win. ==========')
    else:
        print('========== Game Over! You lose. ==========')
    print_end_game()
