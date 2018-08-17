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
public interface Cell {

    /**
     *
     * @return boolean is cell opened
     */
    public boolean isOpen();
    
    /**
     *
     * @return boolean is cell a mine
     */
    public boolean isMine();
    
    public void open();
    
    public void flag();
    
    public void unflag();
    
    /**
     *
     * @return is flag exist
     */
    public boolean isFlag();
    
}
