import java.util.Scanner;
import java.util.Random;
import java.io.*;

public class SimpleMine {

	private final int EMPTY_TILE = 0;
    private final int MINE_TILE = 9;
    private final int COVERED_TILE = 10;
    private final int COVERED_MINE_TILE = 19;


    private int[][] tile;
    private boolean gameStart;
    private int collumnNumber;
    private int mineNumber;
    private int unopened;

    public SimpleMine() {
    	collumnNumber = -1;
    	mineNumber = -1;
    	unopened = 0;
    }

    public void getInput() {
    	printHeader();
    	Scanner in = new Scanner(System.in);
    	while (collumnNumber < 0) {
    	   System.out.print("\nInsert board size: ");
    	   collumnNumber = in.nextInt();
    	   if (collumnNumber<0) {
    	   		System.out.println("Please insert number larger than 0. \n");
    	   } else if (collumnNumber > 30) {
    	   		collumnNumber=-1;
    	   		System.out.println("Sorry, this game is only capable to 30x30 board size. \n");
    	   }
    	}
    	while (mineNumber<0) {
    		System.out.print("Insert number of mines: ");
    		mineNumber = in.nextInt();	
    		if ((mineNumber > collumnNumber*collumnNumber)  || (mineNumber < 0)) {
    			mineNumber = -1;
    			System.out.println("Number of mines should be in range from 0 to " + collumnNumber*collumnNumber + ".\n");
    		}
    	}
    	
    	unopened = collumnNumber*collumnNumber - mineNumber;
    }

     public void printHeader() {
        File f = new File("header.txt");
        try {
        	BufferedReader br = new BufferedReader(new FileReader(f));
	 		String header;
	 		try {
	 			while ((header = br.readLine()) != null) {
	    			System.out.println(header);
	  			}
	  		} catch (IOException ee) {
	  			System.out.println("gagal1");
	  		}	
        } catch (FileNotFoundException e) {
	  			System.out.println("gagal2");
        }
    }

    public void drawBoard() {
    	for (int i=0; i< collumnNumber; i++) {
    		for (int j=0; j<collumnNumber; j++) {
    			if (tile[i][j] == MINE_TILE) 
    				System.out.print(" # ");
    			else if (tile[i][j]>= COVERED_TILE)
    				System.out.print(" - ");
    			else if (tile[i][j]<MINE_TILE)
    				System.out.print(" " + tile[i][j] + " ");
    		}
    		System.out.println();
    	}
    	System.out.println();
    }

    public void initGame() {
    	gameStart = true;
    	Random rand = new Random();
        tile = new int[collumnNumber][collumnNumber];

       for (int i=0; i< collumnNumber; i++) {
    		for (int j=0; j<collumnNumber; j++) {
    			tile[i][j] = COVERED_TILE;
    		}
    	}

    	//add mine to random tile
    	int i=0;
    	while (i < mineNumber) {
    		int idx_i = rand.nextInt(collumnNumber);
    		int idx_j = rand.nextInt(collumnNumber);
    		if (tile[idx_i][idx_j] != COVERED_MINE_TILE){
    			tile[idx_i][idx_j] = COVERED_MINE_TILE;
    			i++;

    			//increase adjacent cells' value
	    		int i_adj;
	    		int j_adj;
	    		if (idx_j< collumnNumber-1) {		//gak rapat kanan
	    			//kanan
	    			i_adj = idx_i;
	    			j_adj = idx_j + 1;
	    			if (tile[i_adj][j_adj] != COVERED_MINE_TILE)
	    				tile[i_adj][j_adj]+=1;

	    			//kanan atas
	    			i_adj = idx_i - 1 ;
	    			j_adj = idx_j + 1;
	    			if ((i_adj>=0) && (tile[i_adj][j_adj] != COVERED_MINE_TILE))
	    				tile[i_adj][j_adj]+=1;

	    			//kanan bawah
	    			i_adj = idx_i + 1;
	    			j_adj = idx_j + 1;
	    			if ((i_adj<collumnNumber) && (tile[i_adj][j_adj] != COVERED_MINE_TILE))
	    				tile[i_adj][j_adj]+=1;
	    		}

	    		if (idx_j > 0) {				//gak paling kiri
	    			//kiri
	    			i_adj = idx_i;
	    			j_adj = idx_j - 1;
	    			if (tile[i_adj][j_adj] != COVERED_MINE_TILE)
	    				tile[i_adj][j_adj]+=1;

	    			//kiri atas
	    			i_adj = idx_i - 1;
	    			j_adj = idx_j - 1;
	    			if ((i_adj>=0) && (tile[i_adj][j_adj] != COVERED_MINE_TILE))
	    				tile[i_adj][j_adj]+=1;

	    			//kiri bawah
	    			i_adj = idx_i + 1;
	    			j_adj = idx_j - 1;
	    			if ((i_adj<collumnNumber) && (tile[i_adj][j_adj] != COVERED_MINE_TILE))
	    				tile[i_adj][j_adj]+=1;

	    		}

	    		//atas
	    		i_adj = idx_i - 1;
				j_adj = idx_j;
				if ((i_adj>=0) && (tile[i_adj][j_adj] != COVERED_MINE_TILE))
					tile[i_adj][j_adj]+=1;
	    		
	    		//bawah
	    		i_adj = idx_i + 1;
				j_adj = idx_j;
				if ((i_adj<collumnNumber) && (tile[i_adj][j_adj] != COVERED_MINE_TILE))
					tile[i_adj][j_adj]+=1;
	    	}
    	}   
    }

    public void showTileValue() {
    	for (int i=0; i< collumnNumber; i++) {
    		for (int j=0; j<collumnNumber; j++) {
    			if (tile[i][j]>=10)
    				tile[i][j] -= 10;
    		}
    	}
    }

    public void startGame() {
       	Scanner in = new Scanner(System.in);
    	System.out.println();
    	System.out.println("--------- G A M E   S T A R T E D ---------");
    	drawBoard();
    	System.out.println("Reminder: There are " + mineNumber + " mines.\n");

    	while (gameStart) {
    		int i=-1;
    		int j= -1;
    		System.out.println("Pick a tile!");
    		while ((i<0) || (i>=collumnNumber)) {
    			System.out.print("Row: ");
    			i = in.nextInt();
    			if ((i<0) || (i>= collumnNumber)) {
    				System.out.println("Row number should be in range from 0 to " + (collumnNumber-1) + " (inclusive). Please try again. \n");
    			}
    		}

    		while ((j<0) || (j>=collumnNumber)) {
    			System.out.print("Collumn: ");
    			j = in.nextInt();
    			if ((j<0) || (j>= collumnNumber)) {
    				System.out.println("Collumn number should be in range from 0 to " + (collumnNumber-1) + " (inclusive). Please try again. \n");
    			}
    		}
    		if (tile[i][j] == COVERED_MINE_TILE) {
    			//milih bom
    			showTileValue();
    			gameStart=false;
      		} else if (tile[i][j]<MINE_TILE) {
    			//milih yang udah di buka
    			System.out.println("You already opened the tile!");
    		} else {
    			//milih yang belum dibuka, tapi bukan mine
    			tile[i][j] -= COVERED_TILE;
    			unopened -=1;
    			if (tile[i][j] == EMPTY_TILE) {
    				findEmptyTiles(i, j);
    			}
    		}
    		System.out.println();
    		drawBoard();
    		System.out.println("Reminder: There are " + mineNumber + " mines.\n");
    	//	System.out.println("unopened Tile: " + unopened + "/" + (collumnNumber*collumnNumber - mineNumber) +".");

    		if (unopened == 0)
    			gameStart= false;
    	}
    	

    	if (unopened == 0) {
    		System.out.println("--------- Y O U  W I N ! ---------");
    	} else {
    		System.out.println("--------- Y O U  L O S E  :( ---------");
    	}
    }

    public void findEmptyTiles(int idx_i, int idx_j) {
    	int i_adj;
		int j_adj;
		if (idx_j< collumnNumber-1) {		//gak rapat kanan
			//kanan
			i_adj = idx_i;
			j_adj = idx_j + 1;
			if (tile[i_adj][j_adj] > MINE_TILE) {
				tile[i_adj][j_adj]-=10;
				if (tile[i_adj][j_adj] == 0)
					findEmptyTiles(i_adj, j_adj);
				unopened -=1;
			}


			//kanan atas
			i_adj = idx_i - 1 ;
			j_adj = idx_j + 1;
			if ((i_adj >=0) && (tile[i_adj][j_adj] > MINE_TILE)) {
				tile[i_adj][j_adj]-=10;
				if (tile[i_adj][j_adj] == 0)
					findEmptyTiles(i_adj, j_adj);
				unopened -=1;
			}

			//kanan bawah
			i_adj = idx_i + 1;
			j_adj = idx_j + 1;
			if ((i_adj < collumnNumber) && (tile[i_adj][j_adj] > MINE_TILE)) {
				tile[i_adj][j_adj]-=10;
				if (tile[i_adj][j_adj] == 0)
					findEmptyTiles(i_adj, j_adj);
				unopened -=1;
			}
		}

		if (idx_j > 0) {				//gak paling kiri
			//kiri
			i_adj = idx_i;
			j_adj = idx_j - 1;
			if (tile[i_adj][j_adj] > MINE_TILE) {
				tile[i_adj][j_adj]-=10;
				if (tile[i_adj][j_adj] == 0)
					findEmptyTiles(i_adj, j_adj);
				unopened -=1;
			}
			//kiri atas
			i_adj = idx_i - 1;
			j_adj = idx_j - 1;
			if ((i_adj >=0) && (tile[i_adj][j_adj] > MINE_TILE)) {
				tile[i_adj][j_adj]-=10;
				if (tile[i_adj][j_adj] == 0)
					findEmptyTiles(i_adj, j_adj);
				unopened -=1;
			}

			//kiri bawah
			i_adj = idx_i + 1;
			j_adj = idx_j - 1;
			if ((i_adj < collumnNumber) && (tile[i_adj][j_adj] > MINE_TILE)) {
				tile[i_adj][j_adj]-=10;
				if (tile[i_adj][j_adj] == 0)
					findEmptyTiles(i_adj, j_adj);
				unopened -=1;
			}
		}

		//atas
		i_adj = idx_i - 1;
		j_adj = idx_j;
		if ((i_adj >=0) && (tile[i_adj][j_adj] > MINE_TILE)) {
			tile[i_adj][j_adj]-=10;
			if (tile[i_adj][j_adj] == 0)
				findEmptyTiles(i_adj, j_adj);
			unopened -=1;
		}

		//bawah
		i_adj = idx_i + 1;
		j_adj = idx_j;
		if ((i_adj < collumnNumber) && (tile[i_adj][j_adj] > MINE_TILE)) {
				tile[i_adj][j_adj]-=10;
				if (tile[i_adj][j_adj] == 0)
					findEmptyTiles(i_adj, j_adj);
				unopened -=1;
			}
	}

 
    public static void main(String[] args) {

    	SimpleMine play = new SimpleMine();
    	play.getInput();
    	play.initGame();
    	play.startGame();
    }
}