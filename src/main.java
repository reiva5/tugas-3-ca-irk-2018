/*
 * @author Aldo Azali
 */
import java.awt.*;
import java.awt.event.*;
import javax.swing.*;
import java.util.Random;



class main extends JFrame implements ActionListener{
	MenuBar mb;
	Menu game,options;
	MenuItem newgame,highscore,exit,easy,medium,hard,custom;
	int level = 1, brs = 8, kol = 8;
	// Button & Bom
	Button[][] b;
	int jumlahBom = 10;
	int[] posisiBom;
	boolean[][] cekBom;
	Random rand = new Random();
	//Label Angka & Bom
	Label[][] l;
	
	main(){
		//Nama Aplikasi
		super("Minesweeper");
		//Menu Bar
		game = new Menu("Game");
		options = new Menu("Options");
		newgame = new MenuItem("New Game");
		highscore = new MenuItem("High Score");
		exit = new MenuItem("Exit");
		easy = new MenuItem("Beginner");
		medium = new MenuItem("Intermediate");
		hard = new MenuItem("Expert");
		custom = new MenuItem("Custom");
		newgame.addActionListener(this);
		highscore.addActionListener(this);
		exit.addActionListener(this);
		easy.addActionListener(this);
		medium.addActionListener(this);
		hard.addActionListener(this);
		custom.addActionListener(this);
		game.add(newgame); game.add(highscore); game.add(exit);
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
		
		//Label Angka
		l = new Label[brs][kol];
		for(int i = 0; i < brs; i++){
			for(int j = 0; j < kol; j++){
				l[i][j] = new Label();
				l[i][j].setBounds(i*20+45,j*20+60,20,20);
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
			int x = posisiBom[i]/brs;
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
		else{
			setSize(275,300);
		}
		setLayout(null);
		setVisible(true);
	}
	
	
	public void actionPerformed(ActionEvent e){
		//Button
		for(int i = 0; i < brs; i++){
			for(int j = 0; j < kol; j++){
				if(e.getSource() == b[i][j]){
					// Pengecekan apakah button yang diklik terdapat bom atau tidak
					if(cekBom[i][j]){
						// Mendelete semua button yang berisi bom
						for(int k = 0; k < jumlahBom; k++){
							int x = posisiBom[k]/brs;
							int y = posisiBom[k]%kol;
							remove(b[x][y]);
						}
						//Menampilkan pesan kekalahan
						JOptionPane.showMessageDialog(this,"Bom Meledak!!!");
					}
					else{
						// Mendelete button yang di klik dan menampilkan jumlah bom di sekitarnya
						remove(b[i][j]);
						b[i][j] = null;
						invalidate();
						
						int countBom = 0;
						
						l[i][j].setText(" ");
					}
				}
			}
		}
		
		// Menu Bar
		if(e.getSource() == newgame){
			
		}
		if(e.getSource() == highscore){
			
		}
		if(e.getSource() == exit){
			
		}
		if(e.getSource() == easy){
			level = 1; brs = 8; kol = 8; jumlahBom = 10;
			JOptionPane.showMessageDialog(this,"Level Easy");
		}
		if(e.getSource() == medium){
			level = 2; brs = 16; kol = 16; jumlahBom = 40;
			JOptionPane.showMessageDialog(this,"Level Medium");
		}
		if(e.getSource() == hard){
			level = 3; brs = 32; kol = 16; jumlahBom = 99;
			JOptionPane.showMessageDialog(this,"Level Hard");
		}
		if(e.getSource() == custom){
			// tampilkan message untuk menentukan pilihan
		}	
			
	}
	
	public static void main(String[] args) {
		Frame f = new main();
	}

}
