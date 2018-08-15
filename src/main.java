import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

public class main extends Frame implements ActionListener{
	MenuBar mb;
	Menu game,options;
	MenuItem newgame,highscore,exit,easy,medium,hard,custom;
	int level = 1, brs = 8, kol = 8;
	// Button
	Button[][] b;
	
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
				b[i][j].setBounds(i*20+55,j*20+100,20,20);
				b[i][j].addActionListener(this);
			}
		}
		for(int i = 0; i < brs; i++){
			for(int j = 0; j < kol; j++){
				add(b[i][j]);
			}
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
		if(e.getSource() == newgame){
			
		}
		if(e.getSource() == highscore){
			
		}
		if(e.getSource() == exit){
			
		}
		if(e.getSource() == easy){
			level = 1;
			brs = 8;
			kol = 8;
		}
		if(e.getSource() == medium){
			level = 2;
			brs = 16;
			kol = 16;
		}
		if(e.getSource() == hard){
			level = 3;
			brs = 32;
			kol = 16;
		}
		if(e.getSource() == custom){
			// tampilkan message untuk menentukan pilihan
		}
		if(e.getSource() == b){
			
		}
		
			
	}
	
	public static void main(String[] args) {
		new main();
	}

}
