/*
 * @author Aldo Azali
 */
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import java.util.Random;



class main extends JFrame implements ActionListener{
	boolean gameOver;
	int sisaButton;
	MenuBar mb;
	Menu game,options;
	MenuItem newgame,highscore,exit,easy,medium,hard,custom;
	static int level = 1, brs = 8, kol = 8;
	// Button & Bom
	Button[][] b;
	static int jumlahBom = 10;
	int[] posisiBom;
	boolean[][] cekBom;
	Random rand = new Random();
	//Label Angka & Bom
	Label[][] l;
	boolean[][] bukaButton;
	
	main(){
		//Nama Aplikasi
		super("Minesweeper");
		gameOver = false;
		sisaButton = (brs*kol) - jumlahBom;
		//Menu Bar
		game = new Menu("Game");
		options = new Menu("Options");
		newgame = new MenuItem("New Game");
		//highscore = new MenuItem("High Score");
		exit = new MenuItem("Exit");
		easy = new MenuItem("Beginner");
		medium = new MenuItem("Intermediate");
		hard = new MenuItem("Expert");
		custom = new MenuItem("Custom");
		newgame.addActionListener(this);
		//highscore.addActionListener(this);
		exit.addActionListener(this);
		easy.addActionListener(this);
		medium.addActionListener(this);
		hard.addActionListener(this);
		custom.addActionListener(this);
		game.add(newgame); /*game.add(highscore); */ game.add(exit);
		options.add(easy);options.add(medium);options.add(hard); options.add(custom);
		
		//Button
		b = new Button[brs][kol];
		for(int i = 0; i < brs; i++){
			for(int j = 0; j < kol; j++){
				b[i][j] = new Button();
				b[i][j].setBounds(i*20+45,j*20+60,20,20);
				b[i][j].addActionListener(this);
			}
		}
		for(int i = 0; i < brs; i++){
			for(int j = 0; j < kol; j++){
				add(b[i][j]);
			}
		}
		
		//Label Angka & bukaButton
		l = new Label[brs][kol];
		bukaButton = new boolean[brs][kol];
		for(int i = 0; i < brs; i++){
			for(int j = 0; j < kol; j++){
				l[i][j] = new Label();
				l[i][j].setBounds(i*20+45,j*20+60,20,20);
				bukaButton[i][j] = false;
			}
		}
		for(int i = 0; i < brs; i++){
			for(int j = 0; j < kol; j++){
				add(l[i][j]);
			}
		}
		
		// Bom
		posisiBom = new int[jumlahBom]; // posisi bom dalam bentuk integer dan akan dikonversi ke bentuk (x,y) berdasarkan jumlah baris dan kolom
		cekBom = new boolean[brs][kol]; // Mengecek apakah di posisi (x,y) ada bom atau tidak
		for(int i = 0; i < jumlahBom; i++){
			boolean cekRandom = false;
			do{
				cekRandom = false;
				posisiBom[i] = rand.nextInt(brs*kol);
				// Pengecekan agar posisi bom tidak berada pada tempat yang sama
				for(int j =0; j < i;j++){
					if(posisiBom[j] == posisiBom[i]){
						cekRandom = true;
					}
				}
			} while(cekRandom);
			System.out.println(posisiBom[i]);
		}
		//Konversi integer ke posisi bom
		for(int i = 0; i < brs; i++){
			for(int j = 0; j < kol; j++){
				cekBom[i][j] = false;
			}
		}
		for(int i = 0; i < jumlahBom; i++){
			int x = posisiBom[i]/kol;
			int y = posisiBom[i]%kol;
			cekBom[x][y] = true;
			l[x][y].setText(" X"); // X menandakan bahwa disitu terdapat bom
			System.out.println(x+" "+y);
		}
		
		
		mb = new MenuBar();
		mb.add(game);
		mb.add(options);
		
		setMenuBar(mb);
		if(level == 1){
			setSize(275,300);
		}
		else if(level == 2){
			setSize(275*2,300*2);
		}
		else if(level == 3){
			setSize(275*3,300*2);
		}
		else{
			setSize(275*3,300*2);
		}
		setLayout(null);
		setVisible(true);
	}
	
	
	public void actionPerformed(ActionEvent e){
		//Button
		if(!gameOver){
			for(int i = 0; i < brs; i++){
				for(int j = 0; j < kol; j++){
					if(e.getSource() == b[i][j]){
						// Pengecekan apakah button yang diklik terdapat bom atau tidak
						if(cekBom[i][j]){
							// Mendelete semua button yang berisi bom
							for(int k = 0; k < jumlahBom; k++){
								int x = posisiBom[k]/kol;
								int y = posisiBom[k]%kol;
								remove(b[x][y]);
							}
							//Menampilkan pesan kekalahan
							JOptionPane.showMessageDialog(this,"Bom Meledak!!! Anda Kalah !!");
							gameOver = true;
						}
						else{
							// Mendelete button yang di klik dan menampilkan jumlah bom di sekitarnya
							countingBom(i,j);
						}
					}
				}
			}
			System.out.println(sisaButton);
			if(sisaButton == 0){
				JOptionPane.showMessageDialog(this,"Bom berhasil diamankan semuanya!! Anda Menang !!");
				gameOver = true;
			}
		}
		
		// Menu Bar
		if(e.getSource() == newgame){
			new main();
			setVisible(false); //you can't see me!
			dispose(); //Destroy the JFrame object
			
		}
	/*	if(e.getSource() == highscore){
			//using file External
			JOptionPane.showMessageDialog(this,"TBD");
		} */
		if(e.getSource() == exit){
			setVisible(false); //you can't see me!
			dispose(); //Destroy the JFrame object
		}
		if(e.getSource() == easy){
			JOptionPane.showMessageDialog(this,"Level Easy");
			setVisible(false); //you can't see me!
			dispose(); //Destroy the JFrame object
			level = 1; brs = 8; kol = 8; jumlahBom = 10;
			new main();
			
		}
		if(e.getSource() == medium){
			JOptionPane.showMessageDialog(this,"Level Medium");
			setVisible(false); //you can't see me!
			dispose(); //Destroy the JFrame object
			level = 2; brs = 16; kol = 16; jumlahBom = 40;
			new main();
		}
		if(e.getSource() == hard){
			JOptionPane.showMessageDialog(this,"Level Hard");
			setVisible(false); //you can't see me!
			dispose(); //Destroy the JFrame object
			level = 3; brs = 32; kol = 16; jumlahBom = 99;
			new main();
		}
		if(e.getSource() == custom){
			// tampilkan message untuk menentukan pilihan
			JOptionPane.showMessageDialog(this,"TBD");
		}
			
	}
	
	//Rekursif untuk menghitung jumlah bom di sekitar kotak yang dipilih
	void countingBom(int x, int y){
		bukaButton[x][y] = true;
		remove(b[x][y]);
		b[x][y] = null;
		sisaButton--;
		//invalidate();
		int countBom = 0;
		// Mengecek 8 sisi sekitar
		if(x+1 < brs && !bukaButton[x+1][y])							// 1
			if(cekBom[x+1][y]){ countBom++; }
		if(x+1 < brs && y+1 < kol && !bukaButton[x+1][y+1])				// 2
			if(cekBom[x+1][y+1]){ countBom++; }
		if(x+1 < brs && y-1 >= 0 && !bukaButton[x+1][y-1])				// 3
			if(cekBom[x+1][y-1]){ countBom++; }
		if(x-1 >= 0 && !bukaButton[x-1][y])								// 4
			if(cekBom[x-1][y]){ countBom++; }
		if(x-1 >= 0 && y+1 < kol && !bukaButton[x-1][y+1])				// 5
			if(cekBom[x-1][y+1]){ countBom++; }
		if(x-1 >= 0 && y-1 >= 0 && !bukaButton[x-1][y-1])				// 6
			if(cekBom[x-1][y-1]){ countBom++; }
		if(y+1 < kol && !bukaButton[x][y+1])							// 7
			if(cekBom[x][y+1]){ countBom++; }
		if(y-1 >= 0 && !bukaButton[x][y-1])								// 8
			if(cekBom[x][y-1]){ countBom++; }
		
		if(countBom == 0){
			l[x][y].setText(" ");
			if(x+1 < brs && !bukaButton[x+1][y])							// 1
				countingBom(x+1,y);
			if(x+1 < brs && y+1 < kol && !bukaButton[x+1][y+1])				// 2
				countingBom(x+1,y+1);
			if(x+1 < brs && y-1 >= 0 && !bukaButton[x+1][y-1])				// 3	
				countingBom(x+1,y-1);
			if(x-1 >= 0 && !bukaButton[x-1][y])								// 4	
				countingBom(x-1,y);
			if(x-1 >= 0 && y+1 < kol && !bukaButton[x-1][y+1])				// 5	
				countingBom(x-1,y+1);
			if(x-1 >= 0 && y-1 >= 0 && !bukaButton[x-1][y-1])				// 6	
				countingBom(x-1,y-1);
			if(y+1 < kol && !bukaButton[x][y+1])							// 7	
				countingBom(x,y+1);
			if(y-1 >= 0 && !bukaButton[x][y-1])								// 8	
				countingBom(x,y-1); 
		}
		else{
			l[x][y].setText(" "+countBom);
		}
	}
	
	public static void main(String[] args) {
		Frame f = new main();
	}

}
