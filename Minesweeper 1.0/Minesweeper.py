#!/usr/bin/python

import random

LEVEL_EASY = 5
LEVEL_MEDIUM = 4
LEVEL_HARD = 3
LEVEL_INSANE = 2

class Point:
	def __init__(self,x,y):
		self.x = x
		self.y = y
	def print_point(self):
		print("(" + str(self.x) + "," + str(self.y) + ")")
	def __eq__(self,other):
		if isinstance(other, Point):
			return self.x == other.x and self.y == other.y
		return NotImplemented
	def __ne__(self,other):
		result = self.__eq__(other)
		if result is NotImplemented:
			return result
		return not result
	def __lt__(self,other):
		result = self.__ne__(other)
		if result is NotImplemented:
			return result
		return self.x < other.x or self.x == other.x and self.y < other.y
	def __gt__(self,other):
		result = self.__ne__(other)
		if result is NotImplemented:
			return result
		return self.x > other.x or self.x == other.x and self.y > other.y
	def getX(self):
		return self.x
	def getY(self):
		return self.y
			
class Minesweeper:
	def making_board():
		column = int(input("How many columns do you want to have? "))
		while(column < 8):
			column = int(input("It's not sufficient. Enter new number: "))
		row = int(input("How many rows do you want to have? "))
		while(row < 8):
			row = int(input("It's not sufficient. Enter new number: "))
		type = int(input("Which level do you want to play? Press 1 for easy, 2 for medium, 3 for hard and 4 for insane: "))
		Minesweeper.play_minesweeper(column, row, type)

	def play_minesweeper(column, row, type):
		if type == 1:
			mines = int(column * row / LEVEL_EASY)
			Minesweeper.play(column, row, mines)
		elif type == 2:
			mines = int(column * row / LEVEL_MEDIUM)
			Minesweeper.play(column, row, mines)
		elif type == 3:
			mines = int(column * row / LEVEL_HARD)
			Minesweeper.play(column, row, mines)
		elif type == 4:
			mines = int(column * row / LEVEL_INSANE)
			Minesweeper.play(column, row, mines)
		else:
			type = input("Input invalid, enter new input: ")
			Minesweeper.play_minesweeper(column, row, type)
	
	def check_list(list1, list2):
		if len(list1) != len(list2):
			return False
		else:
			found_difference = False
			for element in list1:
				if not Minesweeper.search(element, list2):
					found_difference == True
					break
			return not(found_difference)
	
	def print_matrix_adjacent(matrix_adjacent_mines):
		i = 0
		j = 0
		for i in range(len(matrix_adjacent_mines)):
			for j in range(len(matrix_adjacent_mines[0])):
				print(str(matrix_adjacent_mines[i][j]) + " ", end = '')
			print()

	def play(column, row, mines):
		plant = Minesweeper.randomize_mines(column, row, mines)
		print("Map of mines:")
		# Minesweeper.drawmine(column, row, plant)
		matrix_adjacent_mines = Minesweeper.crate_matrix_adjacent_mines(column, row, plant)
		# Minesweeper.print_matrix_adjacent(matrix_adjacent_mines)
		flag = []
		opened = []
		boom = Point(column, row)
		queue_blank_coordinate = []
		print("Let's play!")
		lose = False
		i = 0
		j = 0
		cells =[]
		for i in range(row):
			for j in range(column):
				point = Point(i,j)
				cells.append(point)
		while (not Minesweeper.check_list(plant, flag) or len(cells) - len(opened) - len(flag) != 0) and not lose:
			if(mines - len(flag) > 1):
				print("You have " + str(mines - len(flag)) + " mines left")
			else:
				print("You have " + str(mines - len(flag)) + " mine left")
			Minesweeper.draw_board(column, row, flag, opened, matrix_adjacent_mines, boom, plant)
			lose = Minesweeper.choose(column, row, plant, flag, matrix_adjacent_mines, opened, queue_blank_coordinate)
			print(lose)
		if lose:
			print("Game over!")
		else:
			print("Congratulations, You win!")
			Minesweeper.draw_board(column, row, flag, opened, matrix_adjacent_mines, boom, plant)
			
	def drawmine(column, row, plant):
		i = 0
		j = 0
		for i in range(row):
			for j in range(column):
				point = Point(i,j)
				if Minesweeper.search(point, plant):
					print(". ", end = '')
				else:
					print("_ ", end = '')
			print()

	def randomize_mines(column, row, mines):
		i = 0
		plant = []
		while i < mines:
			temp_column = random.randint(0,column - 1)
			temp_row =	random.randint(0,row - 1)
			point = Point(temp_column, temp_row)
			if Minesweeper.check_unique(point, plant):
				Minesweeper.add_mines(point, plant)
				i += 1
		Minesweeper.print_mines(plant)
		return plant
	
	def crate_matrix_adjacent_mines(column, row, plant):
		matrix_adjacent_mines = []
		for i in range(row):
			temp_row_list_n_mines = []
			for j in range(column):
				point_to_be_checked = Point(i,j)
				if(point_to_be_checked.getX() == 0) and ((point_to_be_checked.getY() != 0) or (point_to_be_checked.getY() != row - 1)): 
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX(), point_to_be_checked.getX() + 2, point_to_be_checked.getY() - 1, point_to_be_checked.getY() + 2)
				elif(point_to_be_checked.getX() == 0) and (point_to_be_checked.getY() == 0):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX(), point_to_be_checked.getX() + 2, point_to_be_checked.getY(), point_to_be_checked.getY() + 2)
				elif(point_to_be_checked.getX() == 0) and (point_to_be_checked.getY() == row):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX(), point_to_be_checked.getX() + 2, point_to_be_checked.getY() - 1, point_to_be_checked.getY() + 1)
				elif(point_to_be_checked.getX() == column - 1) and ((point_to_be_checked.getY() != 0) or (point_to_be_checked.getY() != row - 1)):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX() - 1, point_to_be_checked.getX() + 1, point_to_be_checked.getY() - 1, point_to_be_checked.getY() + 2)
				elif(point_to_be_checked.getX() == column - 1) and (point_to_be_checked.getY() == 0):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX() - 1, point_to_be_checked.getX() + 1, point_to_be_checked.getY(), point_to_be_checked.getY() + 2)
				elif(point_to_be_checked.getX() == column - 1) and (point_to_be_checked.getY() == row - 1):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX() - 1, point_to_be_checked.getX() + 1, point_to_be_checked.getY() - 1, point_to_be_checked.getY() + 1)
				elif(point_to_be_checked.getY() == 0) and ((point_to_be_checked.getX() != 0) or (point_to_be_checked.getX() != column)):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX() - 1, point_to_be_checked.getX() + 2, point_to_be_checked.getY(), point_to_be_checked.getY() + 2)
				elif(point_to_be_checked.getY() == row - 1) and ((point_to_be_checked.getX() != 0) or (point_to_be_checked.getX() != column)):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX() - 1, point_to_be_checked.getX() + 2, point_to_be_checked.getY() - 1, point_to_be_checked.getY() + 1)
				else:
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX() - 1, point_to_be_checked.getX() + 2, point_to_be_checked.getY() - 1, point_to_be_checked.getY() + 2)
				temp_row_list_n_mines.append(n_mines)
			matrix_adjacent_mines.append(temp_row_list_n_mines)
		return matrix_adjacent_mines
			
	def looping_matrix_adjacent_mines(plant, point_to_be_checked, start_x, stop_x, start_y, stop_y):
		i = 0
		j = 0
		n_mines = 0
		for i in range (start_x, stop_x):
			for j in range (start_y, stop_y):
				temp_point = Point(i,j)
				if(temp_point != point_to_be_checked):
					if Minesweeper.search(temp_point, plant):
						n_mines = n_mines + 1
		return n_mines
		
	def check_unique(point, plant):
		for point_mines in plant:
			if point_mines == point:
				return False
				break
		return True 

	def add_mines(point, plant):
		i = 0
		reach_last = False
		if (len(plant) == 0):
			plant.append(point)
		else:
			while point > plant[i]:
				i += 1
				if(i == len(plant)):
					reach_last = True
					plant.insert(i, point)
					break
			if not reach_last:
				plant.insert(i, point)

	def print_mines(plant):
		for point_mines in plant:
			point_mines.print_point()
			
	def draw_board(column, row, flag, opened, matrix_adjacent_mines, boom, plant):
		i = 0
		j = 0
		for i in range(row):
			for j in range(column):
				point = Point(i,j)
				if Minesweeper.search(point, flag):
					print("F ", end = '')
				elif Minesweeper.search(point, opened) and matrix_adjacent_mines[i][j] != 0:
					print(str(matrix_adjacent_mines[i][j]) + " ", end = '')
				elif Minesweeper.search(point, opened) and matrix_adjacent_mines[i][j] == 0:
					print("_ ", end = '')
				elif boom == point:
					print("BOOM! ", end = '')
				elif boom != Point(column, row) and Minesweeper.search(point, plant):
					print("* ", end = '')
				else:
					print("X ", end = '')
			print()

	def search(point, list):
		found = False
		i = 0
		while i < len(list) and not found:
			if(point == list[i]):
				found = True
			i += 1
		return found
		
	def search_index(point, list):
		i = 0
		for i in range (len(list)):
			if(point == list[i]):
				break
		return i
		
	def text_to_point(text):
		data = text.split()
		return Point(int(data[0]), int(data[1]))

	def choose(column, row, plant, flag, matrix_adjacent_mines, opened, queue_blank_coordinate):
		done = False
		queue_blank_coordinate = []
		while(not done):
			text = input("Enter the coordinate: ")
			point = Minesweeper.text_to_point(text)
			while (point.getX() < 0) or (point.getX() > column - 1) or (point.getY() < 0) or (point.getY() > row - 1):
				text = input("Coordinate is incorrect. Please write another coordinate: ")
				point = Minesweeper.text_to_point(text)
			type_choosing = input("What do you want to do with the coordinate? Press 1 to open, 2 for flag/unflag: ")
			if(int(type_choosing) == 1) and (not(Minesweeper.search(point, opened)) and not(Minesweeper.search(point, flag))):
				lose = Minesweeper.open(column, row, point, plant, matrix_adjacent_mines, opened, flag, queue_blank_coordinate)
				done = True
				return lose
			elif Minesweeper.search(point, opened):
				print("The coordinate has been opened.")
			elif(int(type_choosing) == 2) and (not(Minesweeper.search(point, opened))) and (not(Minesweeper.search(point, flag))) and len(plant) - len(flag) == 0:
				print("You cannot flag again. You have to unflag at least a flag first.")
			elif(int(type_choosing) == 2):
				Minesweeper.flag(point, plant, flag, opened)
				done = True
			elif int(type_choosing) == 1 and Minesweeper.search(point, flag):
				print("The coordinate has been flagged.")
			else:
				print("Type is incorrect.")
		
			
	def open(column, row, point, plant, matrix_adjacent_mines, opened, flag, queue_blank_coordinate):
		if Minesweeper.search(point, plant):
			print("You stepped on one of the mines... BOOM!")
			boom = point
			Minesweeper.draw_board(column, row, flag, opened, matrix_adjacent_mines, boom, plant)
			return True
		elif matrix_adjacent_mines[point.getX()][point.getY()] != 0:
			opened.append(point)
			if(len(queue_blank_coordinate) != 0):
				P = queue_blank_coordinate[0]
				queue_blank_coordinate.pop(0)
				Minesweeper.check_adjacent_coordinates(column, row, P, plant, matrix_adjacent_mines, opened, flag, queue_blank_coordinate)
		else:
			opened.append(point)
			Minesweeper.check_adjacent_coordinates(column, row, point, plant, matrix_adjacent_mines, opened, flag, queue_blank_coordinate)
	
	def check_adjacent_coordinates(column, row, point, plant, matrix_adjacent_mines, opened, flag, queue_blank_coordinate):
		if(point.getX() != 0):
			temp_point = Point(point.getX() - 1, point.getY())
			if (not(Minesweeper.search(temp_point, opened))) and (not(Minesweeper.search(temp_point, flag))) and (matrix_adjacent_mines[temp_point.getX()][temp_point.getY()] == 0) and (not(Minesweeper.search(temp_point, queue_blank_coordinate))):
				queue_blank_coordinate.append(temp_point)
			elif (not(Minesweeper.search(temp_point, opened))) and (not(Minesweeper.search(temp_point, flag))) and (matrix_adjacent_mines[temp_point.getX()][temp_point.getY()] != 0) and (not(Minesweeper.search(temp_point, queue_blank_coordinate))):
				opened.append(temp_point)
			else:
				pass
		if(point.getX() != column - 1):
			temp_point = Point(point.getX() + 1, point.getY())
			if (not(Minesweeper.search(temp_point, opened))) and (not(Minesweeper.search(temp_point, flag))) and (matrix_adjacent_mines[temp_point.getX()][temp_point.getY()] == 0) and (not(Minesweeper.search(temp_point, queue_blank_coordinate))):
				queue_blank_coordinate.append(temp_point)
			elif (not(Minesweeper.search(temp_point, opened))) and (not(Minesweeper.search(temp_point, flag))) and (matrix_adjacent_mines[temp_point.getX()][temp_point.getY()] != 0) and (not(Minesweeper.search(temp_point, queue_blank_coordinate))):
				opened.append(temp_point)
			else:
				pass
		if(point.getY() != 0):
			temp_point = Point(point.getX(), point.getY() - 1)
			if (not(Minesweeper.search(temp_point, opened))) and (not(Minesweeper.search(temp_point, flag))) and (matrix_adjacent_mines[temp_point.getX()][temp_point.getY()] == 0) and (not(Minesweeper.search(temp_point, queue_blank_coordinate))):
				queue_blank_coordinate.append(temp_point)
			elif (not(Minesweeper.search(temp_point, opened))) and (not(Minesweeper.search(temp_point, flag))) and (matrix_adjacent_mines[temp_point.getX()][temp_point.getY()] != 0) and (not(Minesweeper.search(temp_point, queue_blank_coordinate))):
				opened.append(temp_point)
			else:
				pass
		if(point.getY() != row - 1):
			temp_point = Point(point.getX(), point.getY() + 1)
			if (not(Minesweeper.search(temp_point, opened))) and (not(Minesweeper.search(temp_point, flag))) and (matrix_adjacent_mines[temp_point.getX()][temp_point.getY()] == 0) and (not(Minesweeper.search(temp_point, queue_blank_coordinate))):
				queue_blank_coordinate.append(temp_point)
			elif (not(Minesweeper.search(temp_point, opened))) and (not(Minesweeper.search(temp_point, flag))) and (matrix_adjacent_mines[temp_point.getX()][temp_point.getY()] != 0) and (not(Minesweeper.search(temp_point, queue_blank_coordinate))):
				opened.append(temp_point)
			else:
				pass
		if(len(queue_blank_coordinate) != 0):
			P = queue_blank_coordinate[0]
			queue_blank_coordinate.pop(0)
			Minesweeper.open(column, row, P, plant, matrix_adjacent_mines, opened, flag, queue_blank_coordinate)
		
	def flag(point, plant, flag):
		if Minesweeper.search(point, flag):
			flag.pop(Minesweeper.search_index(point, flag))
		else:
			flag.append(point)

print("Welcome to minesweeper!")
minesweeper = Minesweeper.making_board()