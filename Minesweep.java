import java.util.ArrayList;
import java.util.Scanner;
import java.util.Random;

class Cell{
	int neighborMineCount;
	boolean isOpened, hasFlag, hasMine;
	Cell(){
		neighborMineCount = 0;
		isOpened = hasFlag = hasMine = false;
	}
}

public class Minesweep{
	static Scanner in; 
	static Cell[][] cells;
	static int N, B, no_mine;
	static boolean won;
	static boolean lost;
	public static void main(String[] args){
		System.out.println("******************************************MAINSWIIPA v1.048596**********************************************");
		char opt = 'y';
		do{	
			System.out.println("\nEnter board size(N) and mine count(B) in \"N B\" format:");
			//Read input cell and mine count
			won = false;
			lost = false;
			in = new Scanner(System.in);
			do{
				String[] input = in.nextLine().split(" ");
				N = Integer.parseInt(input[0]);
				B = Integer.parseInt(input[1]);
			}
			while(B > N * N || B < 0);
			//Create cells
			cells = new Cell[N][N];
			for(int i = 0; i < N; ++i){
				for(int j = 0; j < N; ++j){
					cells[i][j] = new Cell();
				}
			}
			//Assign mine
			int mine_count = 0;
			int no_mine = N * N - B;
			while(mine_count != B){
				int x = randInt(0, N - 1);
				int y = randInt(0, N - 1);
				while(cells[x][y].hasMine){
					x = randInt(0, N - 1);
					y = randInt(0, N - 1);
				}
				cells[x][y].hasMine = true;
				mine_count++;
				//Add neighbor count
				int startX = (x - 1 < 0) ? x : x - 1;
				int startY = (y - 1 < 0) ? y : y - 1;
				int endX = (x + 1 > N - 1) ? x : x + 1;
				int endY = (y + 1 > N - 1) ? y : y + 1;
				for(int i = startX; i <= endX; ++i) {
					for(int j = startY; j <= endY; ++j) {
						if(i != x || j != y){
							cells[i][j].neighborMineCount++;
						}
					}
				}
			}
			//Instructions
			System.out.println("Instructions:");
			System.out.println("The board is a two-dimensional cells, which has a number of mines.");
			System.out.println("Cells have two states, opened and closed.");
			System.out.println("\t1. Closed cells is marked by \"-\".");
			System.out.println("\t2. Opened cells is marked by either \"v\" or its neighbor mine count.");
			System.out.println("\t\t1. \"v\" indicates that there is no mines in the cell's neighborhood.");
			System.out.println("\t\t2. Neighbor mine count indicate that there are mine(s) in the cell's neighborhood.");
			System.out.println("Enter coordinate to open a closed cell and reveal its content.");
			System.out.println("\t1. If it has mine then GAME OVER.");
			System.out.println("\t2. Else, it will be marked as stated above.");
			System.out.println("The game can be won by opening all cells that contain no mine.");
			System.out.println("*Cells containing mine is marked by \"X\", shown after GAME OVER.\n");
			//Show map
			showMap(lost);
			//Game
			while(!won && !lost){
				int x, y;
				do{
					System.out.print("Enter coordinate in \"x y\" format: ");
					String[] input = in.nextLine().split(" ");
					x = Integer.parseInt(input[0]);
					y = Integer.parseInt(input[1]);
				}
				while(!isInRange(x, 0, N - 1) || !isInRange(y, 0, N - 1));
				if(cells[x][y].isOpened){
					System.out.println("Cell is opened, try another coordinate!");
				}
				else{
					if(cells[x][y].hasMine){
						lost = true;
					}
					else{
						no_mine -= openCells(x, y);
					}
				}
				if(no_mine == 0){
					won = true;
				}
				showMap(lost);
			}
			//Result
			if(!lost){
				System.out.println("Congratulations! You won the game!");
			}
			else{
				System.out.println("Game Over! You lost!");
			}
			System.out.println("Would you like to try again? y/n");
			opt = in.next().charAt(0);
		}
		while(opt == 'y');
	}
	public static int openCells(int x, int y){
		int n = 1;
		cells[x][y].isOpened = true;
		if(cells[x][y].neighborMineCount == 0){
			int startX = (x - 1 < 0) ? x : x - 1;
			int startY = (y - 1 < 0) ? y : y - 1;
			int endX = (x + 1 > N - 1) ? x : x + 1;
			int endY = (y + 1 > N - 1) ? y : y + 1;
			for(int i = startX; i <= endX; ++i) {
				for(int j = startY; j <= endY; ++j) {
					if(i != x || j != y){
						if(!cells[i][j].isOpened){
							n += openCells(i, j);
						}
					}
				}
			}
		}
		return n;
	}
	public static void showMap(boolean lose){
		System.out.print(" ");
		for(int i = 0; i < N; ++i){
			System.out.print("\t" + i);
		}
		System.out.println();
		for(int i = 0; i < N; ++i){
			for(int j = 0; j <= N; ++j){
				if(j == 0){
					System.out.print(i);
				}
				else{
					System.out.print("\t");
					if(lose){
						if(cells[i][j - 1].hasMine){
							System.out.print("X");
						}
						else{
							System.out.print(cells[i][j - 1].neighborMineCount);
						}
					}
					else{
						if(cells[i][j - 1].isOpened){
							if(cells[i][j - 1].neighborMineCount == 0){
								System.out.print("v");
							}
							else{
								System.out.print(cells[i][j - 1].neighborMineCount);
							}
						}
						else{
							System.out.print("-");
						}
					}
				}
			}
			System.out.println();
		}
	}
	public static boolean isInRange(int x, int min, int max){
		return x >= min && x <= max;
	}
	public static int randInt(int min, int max) {
		Random r = new Random();
		return r.ints(min, (max + 1)).limit(1).findFirst().getAsInt();
	}
}
