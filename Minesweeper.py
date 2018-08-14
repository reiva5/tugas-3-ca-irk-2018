import random as rn

SPACE = '   '
BOMB = '*'

game_over = False
bomb_map = []
unrevealed = 0
n_bomb = 0
map_size = 0

def init_map() :
    global bomb_map
    global unrevealed

    bomb_map = [[{'field': 0, 'revealed': False} for _ in range(map_size)] for _ in range(map_size)]
    unrevealed = map_size ** 2

    rn.seed()

    for _ in range(n_bomb) :
        # Randomize the location
        bomb_loc = (rn.randint(0, map_size - 1), rn.randint(0, map_size - 1))
        # Re-random if the location is already occupied
        while bomb_map[bomb_loc[0]][bomb_loc[1]]['field'] == BOMB :
            bomb_loc = (rn.randint(0, map_size - 1), rn.randint(0, map_size - 1))
        # Put the bomb on the location
        bomb_map[bomb_loc[0]][bomb_loc[1]]['field'] = BOMB
        # print(bomb_loc)

    for i in range(map_size) :
        for j in range(map_size) :
            if bomb_map[i][j]['field'] != BOMB :
                for k in range(-1, 2) :
                    for l in range(-1, 2) :
                        new_loc = (i+k, j+l)
                        bomb_map[i][j]['field'] += count_bombs(new_loc)

def is_loc_valid(loc) :
    i = loc[0]
    j = loc[1]

    return i >= 0 and i < map_size and j >= 0 and j < map_size

def count_bombs(loc) :
    global bomb_map

    i = loc[0]
    j = loc[1]

    if loc != (0, 0) and is_loc_valid(loc) :
        if bomb_map[i][j]['field'] == BOMB :
            return 1
    
    return 0 

def reveal_map() :
    global bomb_map

    for col in bomb_map :
        for tile in col :
            tile['revealed'] = True

def print_map() :
    global unrevealed
    global n_bomb
    
    print()

    print('Remaining unrevealed field :', unrevealed)
    print('Total bomb :', n_bomb)
    print('-------------------------------------')

    for i in range(-1, len(bomb_map)) :
        for j in range(-1, len(bomb_map[i])) :
            if i == -1 :
                if j > -1 :
                    print(j, end=SPACE)
                else :
                    print(' ', end=SPACE)
            else :
                if j > -1 :
                    if bomb_map[i][j]['revealed'] :
                        print(bomb_map[i][j]['field'], end=SPACE)
                    else :
                        print('#', end=SPACE)
                else :
                    print(i, end=SPACE)
        print()

def reveal_tile(loc) :
    global bomb_map
    global map_size
    global unrevealed

    i = loc[0]
    j = loc[1]

    if is_loc_valid(loc) :
        if not bomb_map[i][j]['revealed'] and bomb_map[i][j]['field'] != BOMB:
            bomb_map[i][j]['revealed'] = True
            unrevealed -= 1
            if bomb_map[i][j]['field'] == 0 :
                # Reveal every squares on the left, right, top, and bottom side of the current selected square
                reveal_tile((loc[0], loc[1] - 1))
                reveal_tile((loc[0], loc[1] + 1))
                reveal_tile((loc[0] - 1, loc[1]))
                reveal_tile((loc[0] + 1, loc[1]))

if __name__ == "__main__" :
    print('Minesweeper Tugas 3 IRK')
    print('Ferdiant Joshua M - 13516047')
    print('======================================================')

    map_size = int(input('Fill in the map size : '))
    n_bomb = int(input('Fill in the number of bombs : '))
    while n_bomb > map_size ** 2 :
        print('Number of bombs must be less than', map_size**2, '!')
        n_bomb = int(input('Fill in the number of bombs : '))

    init_map()

    while unrevealed > n_bomb and not game_over:
        print_map()
        
        i, j = input('Input the location you want to open (i j) : ').split(' ')
        i, j = int(i), int(j)
        while not is_loc_valid((i, j)) or bomb_map[i][j]['revealed'] == True :
            print('Invalid input!')
            i, j = input('Input the location you want to open (i j) : ').split(' ')
            i, j = int(i), int(j)

        if bomb_map[i][j]['field'] == BOMB :
            game_over = True
        else :
            reveal_tile((i, j))

    reveal_map()
    print_map()

    if not game_over :
        print('You win!')
    else :
        print('You dugged a bomb! Better luck next time!')