/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package minesweeper;

import com.sun.javafx.geom.Matrix3f;
import java.util.Scanner;

/**
 *
 * @author Untung Tanujaya
 */
public class Game {
    private boolean win;
    private boolean finish;
    private Board board;
    private int turn = 0;
    
    public Game() {
        win = false;
        finish = false;
        int n,b ;
        System.out.println("Masukkan panjang array = ");
        //n = 8;
        System.out.println("Masukkan banyak mine = ");
        //b = 10;
        board = new Board();
        turn++;
        play();
    }

    public void show() {
        System.out.println("Input = ");
        System.out.println("1. Print Board");
        System.out.println("2. Flag Cell");
        System.out.println("3. Unflag Cell");
        System.out.println("4. Open Cell");
    }
    
    public void action(int key) {
        if (key == 1) {
            board.printMatrix();
        } else if (key == 2) {
            board.printMatrix();
            boolean hasFlagged = false;
            do {
                System.out.println("Pilih Cell di atas, input x dan y: ");                
                Scanner scanner = new Scanner(System.in);
                int xcoor = scanner.nextInt();
                int ycoor = scanner.nextInt();
                Cell cell = board.getElMatrix(xcoor, ycoor);
                if (cell.isFlag()) {
                    System.out.println("Maaf, sel telah diflag"); 
                } else {
                    cell.flag();
                    System.out.println("Sel telah diflag");
                    hasFlagged = true;
                }
            } while (!hasFlagged);
        } else if (key == 3) {
            board.printMatrix();
            boolean hasUnflagged = false;
            do {
                System.out.println("Pilih Cell di atas, input x dan y: ");                
                Scanner scanner = new Scanner(System.in);
                int xcoor = scanner.nextInt();
                int ycoor = scanner.nextInt();
                Cell cell = board.getElMatrix(xcoor, ycoor);
                if (!cell.isFlag()) {
                    System.out.println("Maaf, sel telah diunflag"); 
                } else {
                    cell.unflag();
                    System.out.println("Sel telah diunflag");
                    hasUnflagged = true;
                }
            } while (!hasUnflagged);
        } else if (key == 4) {
            System.out.println("Pilih Cell di atas, input x dan y: ");                
            Scanner scanner = new Scanner(System.in);
            int xcoor = scanner.nextInt();
            int ycoor = scanner.nextInt();
            Cell cell = board.getElMatrix(xcoor, ycoor);
            boolean isMine = board.openCell(cell);
            if (isMine) {
                finish = true;
            } else {
                MineCounter mineCounter = (MineCounter) cell;
                if (mineCounter.isEmpty()) {
                    board.openNearbyCell(mineCounter);
                }
            }
        }
    }
    
    public void play() {
        if (win) {
            System.out.println("Selamat anda menang !");
        } else if (!finish) {
            System.out.println("Maaf anda kalah");
        } else {
            show();
            Scanner scanner = new Scanner(System.in);
            int key = scanner.nextInt();
            action(key);
            turn++;
            play();
        }
    }
}
