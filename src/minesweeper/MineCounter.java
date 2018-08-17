/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package minesweeper;

/**
 *
 * @author Untung Tanujaya
 */
public class MineCounter implements Cell {
    private final boolean isMine = false;
    private boolean isOpen;
    private int count;
    private boolean flag;
    private int xcoor;
    private int ycoor;

    public MineCounter() {
        this.count = 0;
        this.isOpen = false;
        this.flag = false;
    }
    
    public MineCounter(int x, int y) {
        this.count = 0;
        this.isOpen = false;
        this.flag = false;
        xcoor = x;
        ycoor = y;
    }
    
    public boolean isEmpty() {
        return count == 0;
    }
    
    public void setCount(int count) {
        this.count = count;
    }

    public int getCount() {
        return count;
    }
    
    public void setXcoor(int xcoor) {
        this.xcoor = xcoor;
    }

    public void setYcoor(int ycoor) {
        this.ycoor = ycoor;
    }

    public int getXcoor() {
        return xcoor;
    }

    public int getYcoor() {
        return ycoor;
    }
    

    @Override
    public boolean isMine() {return isMine;}
    
    @Override
    public boolean isOpen() {
        return isOpen;
    }

    @Override
    public void open() {
        this.isOpen = true;
    }

    @Override
    public void flag() {
        flag = true;
    }

    @Override
    public void unflag() {
        flag = false;
    }

    @Override
    public boolean isFlag() {
        return flag;
    }
}
