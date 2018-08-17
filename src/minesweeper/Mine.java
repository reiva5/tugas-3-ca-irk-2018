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
public class Mine implements Cell {
    private final boolean isMine;
    private boolean isOpen;
    private boolean flag;
    private int xcoor;
    private int ycoor;

    public Mine() {
        this.isMine = true;
        this.isOpen = false;
        this.flag = false;
    }
    
    public Mine(int x, int y) {
        this.isMine = true;
        this.isOpen = false;
        this.flag = false;
        xcoor = x;
        ycoor = y;
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
