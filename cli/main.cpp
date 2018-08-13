#include <iostream>
#include <vector>
#include <cstdlib>
#include <queue>
#include <utility>

using namespace std;

const int dx[] = {0,0,1,-1,1,-1,1,-1};
const int dy[] = {1,-1,0,0,1,-1,-1,1};

template<class T>
void create_table(vector<vector<T>> &tmp, int n, int m, T val){
	tmp.assign(n, vector<T>(m, val));
}
void printTitle();
void fill_bomb(int, int, int, vector<vector<bool>> &);
void readTableSize(int &, int &);
void readNumberOfBomb(int, int, int &);
void fill_numbTable(int, int, vector<vector<short>> &, const vector<vector<bool>> &);
void printMap(int, int, const vector<vector<short>> &, const vector<vector<bool>> &, bool);
void readCoordinateInput(int, int, int &, int &);
void readPlayAgainResponse(bool &);
void open_cell(int, int, int, int, const vector<vector<short>> &, vector<vector<bool>> &, int &);
void refreshScreen();

int main(){
	vector<vector<bool>> isopen, isbomb;
	vector<vector<short>> numb;
	int n, m, nbomb, rem_block;
	bool playing;
	int x, y;
	srand(time(NULL));
	printTitle();
	do{
		playing = false;
		readTableSize(n ,m);
		create_table(isopen, n, m, false);
		create_table(isbomb, n, m, false);
		create_table(numb, n, m, (short)0);
		readNumberOfBomb(n, m, nbomb);
		rem_block = n*m-nbomb;
		fill_bomb(n, m, nbomb, isbomb);
		fill_numbTable(n, m, numb, isbomb);
		while(rem_block > 0){
			refreshScreen();
			printMap(n, n, numb, isopen, false);
			readCoordinateInput(n, m, x, y);
			while(isopen[x][y]){
				cout<<"Kotak telah dibuka, ";
				readCoordinateInput(n, m, x, y);
			}
			if(isbomb[x][y])
				break;
			open_cell(x, y, n, m, numb, isopen, rem_block);
		}
		if(rem_block == 0)
			cout<<"Selamat, Anda Menang!\n";
		else{
			cout<<"Anda Kalah!\n";
			printMap(n, n, numb, isopen, true);
		}
		readPlayAgainResponse(playing);
	}while(playing);
	return 0;
}

void printTitle(){
	cout<<"////////////////////////////////\n";
	cout<<"////////// Minesweeper /////////\n";
	cout<<"////////////////////////////////\n\n";
}

void fill_bomb(int n, int m, int num, vector<vector<bool>> &isbomb){
	while(num){
		int x, y;
		do{
			x = rand()%n;
			y = rand()%m;
		}while(isbomb[x][y]);
		isbomb[x][y] = true;
		num--;
	}
}

void readTableSize(int &n, int &m){
	cout<<"Masukan ukuran kotak NxM\n";
	cout<<"N : "<<flush; cin>>n;
	cout<<"M : "<<flush; cin>>m;
	while(n <= 0 || m <= 0){
		cout<<"Masukan salah! Masukan ukuran kotak NxM\n";
		cout<<"N : "<<flush; cin>>n;
		cout<<"M : "<<flush; cin>>m;
	}
}

void readNumberOfBomb(int n, int m, int &nbomb){
	cout<<"Masukkan banyak bomb : "; cin>>nbomb;
	while(nbomb<0 || nbomb>n*m){
		cout<<"Masukkan salah! masukkan lagi banyak bomb : "; cin>>nbomb;
	}
}

void fill_numbTable(int n, int m, vector<vector<short>> &numb, const vector<vector<bool>> &isbomb){
	for(int i=0;i<n;++i){
		for(int j=0;j<m;++j){
			if(isbomb[i][j])
				numb[i][j] = -1;
			else{
				numb[i][j] = 0;
				for(int k=0;k<8;++k){
					int xx = i+dx[k];
					int yy = j+dy[k];
					if(xx>=0 && xx<n && yy>=0 && yy<m && isbomb[xx][yy])
						++numb[i][j];
				}
			}
		}
	}
}

void printMap(int n, int m, const vector<vector<short>> & numb, const vector<vector<bool>> &isopen, bool openbomb){
	cout<<"\\X\n";
	cout<<"Y";
	for(int i=0;i<n;++i){
		if(i > 0)
			cout<<" ";
		for(int j=0;j<m;++j){
			if(numb[i][j] == -1 && openbomb)
				cout<<"*";
			else if(isopen[i][j])
				cout<<numb[i][j];
			else
				cout<<"#";
		}
		cout<<"\n";
	}
	cout<<"Info : (# : cell yang belum dibuka), ([0-8] : cell yang sudah dibuka dan angka menandakan banyak bomb disekitar cell), (* : bomb)\n";
}

void readCoordinateInput(int n, int m, int &x, int &y){
	cout<<"Masukkan koordinat :\n";
	cout<<"X ["<<0<<".."<<m-1<<"] : "; cin>>y;
	cout<<"Y ["<<0<<".."<<n-1<<"] : "; cin>>x;
	while(x < 0 || x>=n || y < 0 || y >= m){
		cout<<"Masukkan salah! Masukkan lagi koordinat yang valid :\n";
		cout<<"X ["<<0<<".."<<m-1<<"] : "; cin>>y;
		cout<<"Y ["<<0<<".."<<n-1<<"] : "; cin>>x;
	}
}

void readPlayAgainResponse(bool &playing){
	string s;
	cout<<"\nMau main lagi?(y/n) "; cin>>s;
	while(s != "y" && s != "Y" && s !=  "N" && s != "n"){
		cout<<"Input Salah! Mau main lagi?(y/n) "; cin>>s;
	}
	if(s=="Y" || s=="y")
		playing = true;
}

void open_cell(int x, int y, int n, int m, const vector<vector<short>> &numb, vector<vector<bool>> &isopen, int &rem_block){
	queue<pair<int, int>> q;
	q.push({x, y});
	isopen[x][y] = true;
	while(!q.empty()){
		pair<int, int> tmp = q.front();
		q.pop();
		--rem_block;
		if(numb[tmp.first][tmp.second]==0){
			for(int i=0;i<8;++i){
				int xx = tmp.first+dx[i];
				int yy = tmp.second+dy[i];
				if(xx>=0 && xx<n && yy>=0 && yy<m && !isopen[xx][yy]){
					q.push({xx, yy});
					isopen[xx][yy] = true;
				}
			}
		}
	}
}

void refreshScreen(){
	// if(system("CLS")) system("clear");
	cout<<"\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n";
}