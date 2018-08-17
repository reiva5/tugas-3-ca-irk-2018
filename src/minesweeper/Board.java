/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package minesweeper;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Random;

/**
 *
 * @author Untung Tanujaya
 */
public class Board {
    private ArrayList<ArrayList<Cell>> matrix;
    private int n, b;

    public Board() {
        n = 10;
        b = 8;
        matrix = new ArrayList<> (n);
        matrix.forEach((arr) -> {
            arr = new ArrayList<> (n);
        });
        this.setMine(n, b);
    }

    
    
    public Board(int n, int b) {
        this.n = n;
        this.b = b;
        matrix = new ArrayList<> (n);
        matrix.forEach((arr) -> {
            arr = new ArrayList<> (n);
        });
        this.setMine(n, b);
    }
    
    public void setMine (int n, int b) {
        ArrayList<Integer> shuffleXList = new ArrayList<> (b);
        Random random = new Random();
        for (int i = 0; i < b; i++) {
            shuffleXList.add(i % n);
        }
        Collections.shuffle(shuffleXList);
        for (Integer xcoor : shuffleXList) {
            ArrayList<Integer> shuffleYList;
            shuffleYList = (ArrayList<Integer>) shuffleXList.clone();
            shuffleYList.remove(xcoor);
            Random randomList = new Random();
            int ycoor = shuffleYList.get(randomList.nextInt(shuffleYList.size()));
            Mine mine = new Mine(xcoor, ycoor);
            ArrayList<Cell> xArrayList = matrix.get(xcoor);
            xArrayList.set(ycoor, mine);
        }
        for (int xcoor = 0; xcoor < n; xcoor++) {
            ArrayList<Cell> xArrayList = matrix.get(xcoor);
            for (int ycoor = 0; ycoor < n; ycoor++) {
                Cell cell = xArrayList.get(ycoor);
                if (!cell.isMine()) {
                    MineCounter mineCounter = new MineCounter(xcoor, ycoor);
                    int count = countNearbyMines(mineCounter);
                    mineCounter.setCount(count);
                    xArrayList.set(ycoor, mineCounter);
                }
            }
        }
    }
    
    public boolean openCell(Cell cell) {
        cell.open();
        return cell.isMine();
    }

    public void printMatrix() {
        for (ArrayList<Cell> arrayList : matrix) {
            System.out.println("|");
            for (Cell cell : arrayList) {
                if (cell.isOpen()) {
                    if (cell.isMine()) {
                        System.out.print("*");
                    } else {
                        MineCounter mineCounter = (MineCounter) cell;
                        System.out.print(mineCounter.getCount());
                    }
                } else {
                    if (cell.isFlag()) {
                        System.out.print("F");
                    } else {
                        System.out.print("-");
                    }
                }
            }
            System.out.println("|");
        }
    }
    
    public int countNearbyMines (MineCounter mineCounter) {
        int count = 0;
        int xcoor = mineCounter.getXcoor();
        int ycoor = mineCounter.getYcoor();
        for (int i = xcoor - 1; i <= xcoor + 1; i++) {
            for (int j = ycoor - 1; j <= ycoor + 1; j++) {
                if(i >= 0 && i < n && j >= 0 && j < n) {
                    ArrayList<Cell> xArrayList = matrix.get(i);
                    Cell cell = xArrayList.get(j);
                    if (cell.isMine()) {
                        count++;
                    }
                }
            }
        }
        return count;
    }

    public ArrayList<ArrayList<Cell>> getMatrix() {
        return matrix;
    }
    
    public void setElMatrix(Cell cell, int x, int y) {
        ArrayList<Cell> xArrayList = matrix.get(x);
        xArrayList.set(y, cell);
    }
    
    public Cell getElMatrix(int x, int y) {
        ArrayList<Cell> xArrayList = matrix.get(x);
        return xArrayList.get(y);
    }

    public void openNearbyCell(MineCounter mineCounter) {
        int xcoor = mineCounter.getXcoor();
        int ycoor = mineCounter.getYcoor();
        for (int i = xcoor - 1; i <= xcoor + 1; i++) {
            for (int j = ycoor - 1; j <= ycoor + 1; j++) {
                if(i >= 0 && i < n && j >= 0 && j < n) {
                    ArrayList<Cell> xArrayList = matrix.get(i);
                    Cell cell = xArrayList.get(j);
                    if (!cell.isMine()) {
                        if (!cell.isOpen()) {
                            cell.open();
                            MineCounter mineCounter1 = (MineCounter) cell;
                            if (mineCounter1.isEmpty()) {
                                openNearbyCell(mineCounter1);
                            }
                        }
                    }
                }
            }
        }
    }
}
