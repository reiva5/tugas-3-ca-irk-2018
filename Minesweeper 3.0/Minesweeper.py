#!/usr/bin/python
import random, time
from tkinter import *
from threading import Timer
from PIL import Image, ImageTk

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
		N = int(input("Enter the size of grid: "))
		while(N < 8):
			N = int(input("It's not sufficient. Enter new number: "))
		type = int(input("Which level do you want to play? Press 1 for easy, 2 for medium, 3 for hard, 4 for insane and 5 for custom: "))
		Minesweeper.play_minesweeper(N, type)

	def play_minesweeper(N, type):
		if type == 1:
			mines = int(N * N / LEVEL_EASY)
			Minesweeper.play(N, mines)
		elif type == 2:
			mines = int(N * N / LEVEL_MEDIUM)
			Minesweeper.play(N, mines)
		elif type == 3:
			mines = int(N * N / LEVEL_HARD)
			Minesweeper.play(N, mines)
		elif type == 4:
			mines = int(N * N / LEVEL_INSANE)
			Minesweeper.play(N, mines)
		elif type == 5:
			mines = int(input("How many mines do you want to have? "))
			while(mines <= 0) or (mines > N * N):
				if(mines <= 0):
					print("You must at least plant 1 mine. ", end = '')
				else:
					print("The number of mines cannot be larger than total cells. ", end = '')
				mines = int(input("How many mines do you want to have? "))
			Minesweeper.play(N, mines)
		else:
			type = input("Input invalid, enter new input: ")
			Minesweeper.play_minesweeper(N, type)
	
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

	def play(N, mines):
		plant = Minesweeper.randomize_mines(N, mines)
		# print("Map of mines:")
		# Minesweeper.drawmine(N, plant)
		matrix_adjacent_mines = Minesweeper.crate_matrix_adjacent_mines(N, plant)
		# Minesweeper.print_matrix_adjacent(matrix_adjacent_mines)
		flag = []
		opened = []
		boom = Point(N, N)
		queue_blank_coordinate = []
		print("Let's play!")
		lose = False
		i = 0
		j = 0
		cells =[]
		for i in range(N):
			for j in range(N):
				point = Point(i,j)
				cells.append(point)
		have_timed = False
		while (not Minesweeper.check_list(plant, flag) or len(cells) - len(opened) - len(flag) != 0) and not lose:
			if(mines - len(flag) > 1):
				print("You have " + str(mines - len(flag)) + " mines left")
			else:
				print("You have " + str(mines - len(flag)) + " mine left")
			Minesweeper.draw_board(N, flag, opened, matrix_adjacent_mines, boom, plant)
			if(not have_timed):
				time1 = time.time()
				have_timed = True
			lose = Minesweeper.choose(N, plant, flag, matrix_adjacent_mines, opened, queue_blank_coordinate)
		if lose:
			print("Game over!")
			time2 = time.time()
			print("You have played for " + str(time2 - time1) + " seconds")
		else:
			print("Congratulations, You win!")
			Minesweeper.draw_board(N, flag, opened, matrix_adjacent_mines, boom, plant)
		time2 = time.time()
		print("You have played for " + str(time2 - time1) + " seconds")
		enter = input("Please press enter to continue: ")
		main = GUIMainProgram()
			
	def drawmine(N, plant):
		i = 0
		j = 0
		for i in range(N):
			for j in range(N):
				point = Point(i,j)
				if Minesweeper.search(point, plant):
					print(". ", end = '')
				else:
					print("_ ", end = '')
			print()

	def randomize_mines(N, mines):
		i = 0
		plant = []
		while i < mines:
			temp1_N = random.randint(0,N - 1)
			temp2_N =random.randint(0,N - 1)
			point = Point(temp1_N, temp2_N)
			if Minesweeper.check_unique(point, plant):
				Minesweeper.add_mines(point, plant)
				i += 1
		#Minesweeper.print_mines(plant)
		return plant
	
	def crate_matrix_adjacent_mines(N, plant):
		matrix_adjacent_mines = []
		for i in range(N):
			temp_row_list_n_mines = []
			for j in range(N):
				point_to_be_checked = Point(i,j)
				if(point_to_be_checked.getX() == 0) and ((point_to_be_checked.getY() != 0) or (point_to_be_checked.getY() != N - 1)): 
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX(), point_to_be_checked.getX() + 2, point_to_be_checked.getY() - 1, point_to_be_checked.getY() + 2)
				elif(point_to_be_checked.getX() == 0) and (point_to_be_checked.getY() == 0):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX(), point_to_be_checked.getX() + 2, point_to_be_checked.getY(), point_to_be_checked.getY() + 2)
				elif(point_to_be_checked.getX() == 0) and (point_to_be_checked.getY() == N):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX(), point_to_be_checked.getX() + 2, point_to_be_checked.getY() - 1, point_to_be_checked.getY() + 1)
				elif(point_to_be_checked.getX() == N - 1) and ((point_to_be_checked.getY() != 0) or (point_to_be_checked.getY() != N - 1)):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX() - 1, point_to_be_checked.getX() + 1, point_to_be_checked.getY() - 1, point_to_be_checked.getY() + 2)
				elif(point_to_be_checked.getX() == N - 1) and (point_to_be_checked.getY() == 0):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX() - 1, point_to_be_checked.getX() + 1, point_to_be_checked.getY(), point_to_be_checked.getY() + 2)
				elif(point_to_be_checked.getX() == N - 1) and (point_to_be_checked.getY() == N - 1):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX() - 1, point_to_be_checked.getX() + 1, point_to_be_checked.getY() - 1, point_to_be_checked.getY() + 1)
				elif(point_to_be_checked.getY() == 0) and ((point_to_be_checked.getX() != 0) or (point_to_be_checked.getX() != N)):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX() - 1, point_to_be_checked.getX() + 2, point_to_be_checked.getY(), point_to_be_checked.getY() + 2)
				elif(point_to_be_checked.getY() == N - 1) and ((point_to_be_checked.getX() != 0) or (point_to_be_checked.getX() != N)):
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX() - 1, point_to_be_checked.getX() + 2, point_to_be_checked.getY() - 1, point_to_be_checked.getY() + 1)
				else:
					n_mines = Minesweeper.looping_matrix_adjacent_mines(plant, point_to_be_checked, point_to_be_checked.getX() - 1, point_to_be_checked.getX() + 2, point_to_be_checked.getY() - 1, point_to_be_checked.getY() + 2)
				temp_row_list_n_mines.append(n_mines)
			matrix_adjacent_mines.append(temp_row_list_n_mines)
		return matrix_adjacent_mines
			
	def looping_matrix_adjacent_mines(plant, point_to_be_checked, start_x, sroot_x, start_y, sroot_y):
		i = 0
		j = 0
		n_mines = 0
		for i in range (start_x, sroot_x):
			for j in range (start_y, sroot_y):
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
			
	def draw_board(N, flag, opened, matrix_adjacent_mines, boom, plant):
		i = 0
		j = 0
		for i in range(N):
			for j in range(N):
				point = Point(i,j)
				if Minesweeper.search(point, flag):
					print("F ", end = '')
				elif Minesweeper.search(point, opened) and matrix_adjacent_mines[i][j] != 0:
					print(str(matrix_adjacent_mines[i][j]) + " ", end = '')
				elif Minesweeper.search(point, opened) and matrix_adjacent_mines[i][j] == 0:
					print("_ ", end = '')
				elif boom == point:
					print("BOOM! ", end = '')
				elif boom != Point(N, N) and Minesweeper.search(point, plant):
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

	def choose(N, plant, flag, matrix_adjacent_mines, opened, queue_blank_coordinate):
		done = False
		queue_blank_coordinate = []
		while(not done):
			text = input("Enter the coordinate: ")
			point = Minesweeper.text_to_point(text)
			while (point.getX() < 0) or (point.getX() > N - 1) or (point.getY() < 0) or (point.getY() > N - 1):
				text = input("Coordinate is incorrect. Please write another coordinate: ")
				point = Minesweeper.text_to_point(text)
			type_choosing = input("What do you want to do with the coordinate? Press 1 to open, 2 for flag/unflag: ")
			if(int(type_choosing) == 1) and (not(Minesweeper.search(point, opened)) and not(Minesweeper.search(point, flag))):
				lose = Minesweeper.open(N, point, plant, matrix_adjacent_mines, opened, flag, queue_blank_coordinate)
				done = True
				return lose
			elif Minesweeper.search(point, opened):
				print("The coordinate has been opened.")
			elif(int(type_choosing) == 2) and (not(Minesweeper.search(point, opened))) and (not(Minesweeper.search(point, flag))) and len(plant) - len(flag) == 0:
				print("You cannot flag again. You have to unflag at least a flag first.")
			elif(int(type_choosing) == 2):
				Minesweeper.flag(point, plant, flag)
				done = True
			elif int(type_choosing) == 1 and Minesweeper.search(point, flag):
				print("The coordinate has been flagged.")
			else:
				print("Type is incorrect.")
		
			
	def open(N, point, plant, matrix_adjacent_mines, opened, flag, queue_blank_coordinate):
		if Minesweeper.search(point, plant):
			print("You stepped on one of the mines... BOOM!")
			boom = point
			Minesweeper.draw_board(N, flag, opened, matrix_adjacent_mines, boom, plant)
			return True
		elif matrix_adjacent_mines[point.getX()][point.getY()] != 0:
			opened.append(point)
			if(len(queue_blank_coordinate) != 0):
				P = queue_blank_coordinate[0]
				queue_blank_coordinate.pop(0)
				Minesweeper.check_adjacent_coordinates(N, P, plant, matrix_adjacent_mines, opened, flag, queue_blank_coordinate)
		else:
			opened.append(point)
			Minesweeper.check_adjacent_coordinates(N, point, plant, matrix_adjacent_mines, opened, flag, queue_blank_coordinate)
	
	def check_adjacent_coordinates(N, point, plant, matrix_adjacent_mines, opened, flag, queue_blank_coordinate):
		if(point.getX() != 0):
			temp_point = Point(point.getX() - 1, point.getY())
			if (not(Minesweeper.search(temp_point, opened))) and (not(Minesweeper.search(temp_point, flag))) and (matrix_adjacent_mines[temp_point.getX()][temp_point.getY()] == 0) and (not(Minesweeper.search(temp_point, queue_blank_coordinate))):
				queue_blank_coordinate.append(temp_point)
			elif (not(Minesweeper.search(temp_point, opened))) and (not(Minesweeper.search(temp_point, flag))) and (matrix_adjacent_mines[temp_point.getX()][temp_point.getY()] != 0) and (not(Minesweeper.search(temp_point, queue_blank_coordinate))):
				opened.append(temp_point)
			else:
				pass
		if(point.getX() != N - 1):
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
		if(point.getY() != N - 1):
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
			Minesweeper.open(N, P, plant, matrix_adjacent_mines, opened, flag, queue_blank_coordinate)
		
	def flag(point, plant, flag):
		if Minesweeper.search(point, flag):
			flag.pop(Minesweeper.search_index(point, flag))
		else:
			flag.append(point)
			
class GUIMainProgram:
	def __init__(self):
		self.root = Tk()
		self.root.title("Minesweeper")
		width_main_program = '800'
		height_main_program = '800'
		self.root.resizable(0, 0)
		self.root.geometry('{}x{}'.format(width_main_program, height_main_program))
		self.resizingimage('Logo.png',int(width_main_program))
		B = Button(self.root, text = "New Game", command = self.NewGameButtonCallback, height = 4, width = 10, bd = 5, state = ACTIVE, font = 80 )
		B.place(relx=0.5, rely=0.2, anchor = CENTER)
		#B = Button(root, text = "Load Game", command = LoadGameButtonCallback, height = 3, width = 10, bd = 5, state = ACTIVE, font = 80 )
		#B.place(x = 350,y = 250)
		#B = Button(root, text = "High Score", command = HighScoreButtonCallback, height = 3, width = 10, bd = 5, state = ACTIVE, font = 80 )
		#B.place(x = 350,y = 400)
		B = Button(self.root, text = "How to play", command = self.HowtoPlayButtonCallback, height = 4, width = 10, bd = 5, state = ACTIVE, font = 80 )
		B.place(relx=0.5, rely=0.5, anchor=CENTER)
		B = Button(self.root, text = "Quit", command = self.QuitButtonCallback, height = 4, width = 10, bd = 5, state = ACTIVE, font = 80 )
		B.place(relx=0.5, rely=0.8, anchor=CENTER)
		self.root.mainloop()
		
	def resizingimage(self, namefile, width):
		image = Image.open(namefile)
		im = image.size
		ratio = im[0] // width
		image_to_be_shown = image.resize((width,im[1] // ratio), Image.ANTIALIAS)
		self.image = ImageTk.PhotoImage(image_to_be_shown)
		label = Label(image = self.image, padx = 5, pady = 5)
		label.grid()
		
	def NewGameButtonCallback(self):
		self.root.destroy()
		Minesweeper.making_board()
		
	def HowtoPlayButtonCallback(self):
		self.root.withdraw()
		self.top = Toplevel(self.root)
		self.top.title("How to Play")
		width_HowtoPlayMenu = '400'
		height_HowtoPlayMenu = '400'
		self.top.geometry('{}x{}'.format(width_HowtoPlayMenu, height_HowtoPlayMenu))
		self.top.resizable(0, 0)
		text = Text(self.top)
		text.insert(INSERT, "Minesweeper\nBy Kevin Andrian Liwinata\nNIM: 13516118\n\n\nThe objective of minesweeper is to flag all the\nmines so they cannot explode and open the\nremaining cells. You can open a cell by left\nclick mouse and flag by right click mouse.")
		text.grid(padx = 5, pady = 5, sticky = W + S)
		self.top.B = Button(self.top, text = "Back to Main Menu", command = self.BacktoMainMenuCallback, height = 3, width = int(width_HowtoPlayMenu)//20 , bd = 5, state = ACTIVE, font = 80 )
		self.top.B.place(relx=0.5, rely=0.6, anchor=CENTER)
		self.top.mainloop()
		
	def BacktoMainMenuCallback(self):
		self.top.destroy()
		self.root.deiconify()
		
	def QuitButtonCallback(self):
		self.root.destroy()
		
main = GUIMainProgram()