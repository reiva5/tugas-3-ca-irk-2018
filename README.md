# Tugas 3 Ca-IRK 2018

## Dikerjakan Oleh
	Nama : Hagai Raja Sinulingga
	NIM  : 13516136

## Deskripsi
Dalam tugas 3 ini, Ca-IRK harus membuat sebuah game minesweeper. Bagi Ca-IRK yang tidak mengetahui game minesweeper atau ingin mencoba bermain, kalian dapat mencoba di http://www.freeminesweeper.org/minecore.html.

Inti dari permainan minesweeper adalah disediakan _board_ berisi kumpulan kotak. Kotak dapat berisi _mine_ atau tidak. Tujuan pemain adalah untuk menghindari _mine_ dan membuka semua kotak yang tidak berisi _mine_. Kotak yang berisi _mine_ akan menyebabkan kekalahan ketika diklik. Sedangkan kotak yang tidak berisi mine akan berisi angka yang memiliki arti jumlah tetangga yang memiliki _mine_. Tetangga dalam permainan ini adalah kotak yang bersinggungan di minimal satu titik. Maka, jumlah angka maksimal yang muncul adalah 8.

## Spesifikasi
1. Aplikasi menerima 2 buah input, yaitu `N` dan `B`. `N` adalah ukuran _board_. _Board_ akan berbentuk persegi `N`x`N`. Sedangkan `B` adalah jumlah dari _mines_ yang terdapat di _board_.
2. Letak _mine_ akan diacak di awal permainan. _Mine_ dalam _board_ harus pas berjumlah `B` buah.
3. Permainan akan meminta koordinat dari pemain berupa 2 buah angka `x`, `y`, yaitu kotak yang akan dibuka.
4. Jika kotak berisi _mine_ dibuka, permainan berakhir dengan kekalahan dan program harus memunculkan konfigurasi kotak saat itu beserta letak seluruh _mine_.
5. Jika yang dibuka adalah kotak yang disekitarnya tidak ada _mine_, program akan membuka petak lain di sekitarnya sampai bertemu petak yang sebelahnya ada _mine_. (Lihat pada dokumen berikut https://docs.google.com/document/d/1vTz4ZMpc1E6lccPg7Zc87h6Zyf0YZoy2tq1Hym5-yMs/edit)
6. Jika semua kotak yang tidak berisi _mine_ selesai dibuka, tampilkan halaman kemenangan.
7. Bahasa, tampilan, dan lain sebagainya dibebaskan. _Library_ yang akan digunakan juga dibebaskan dan akan masuk penilaian. Kalian dapat membuat _web application_, _desktop application_, _mobile application_, atau bahkan _console_ sekalipun.
8. _Deadline_ dari pengerjaan tugas ini adalah pada Rabu, 15 Agustus 2018 pukul 23.59.
9. Teknis pengerjaan adalah dengan melakukan `fork` pada repository ini dan melakukan `Pull Request` sebelum deadline pada poin 8.
 
## Cara Meng-compile
Program dibuat dalam bahasa C++ dengan standar library iostream, stdlib, stdio dan time untuk OS Linux Ubuntu. Untuk itu diharapkan sudah menginstall C++ compiler pada OS Linux anda.

Jika sudah, masuk ke folder src dalam terminal. Ketik command berikut.
	
	g++ minesweeper.cpp -o minesweeper

## Cara Me-run
Setelah mengcompile silahkan ketik command berikut di dalam folder yang sama.
	
	./minesweeper

## Aturan Bermain
1. Diawal akan diminta nilai N yang adalah ukuran NxN board Minesweeper. Ukuran ini diberi range 1 - 100.
2. Selanjutnya akan diminta nilai B yaitu jumlah mines yang diinginkan. Ukuran ini diberi range 1 - (NxN-1).
3. Selanjutnya board akan disiapkan dan ditampilkan.
4. Dalam setiap turn, akan diminta masukan (x,y) yaitu koordinat yang hendak di klik, boolean click kanan atau kiri dan terakhir akan dikonfirmasi command untuk turn itu.
5. Sisanya sama dengan standar aturan minesweeper.

## Diberikan Oleh
-- `Catherine` Almira - `Dewita` Sonya Tarabunga - `Erick` Wijaya - `Felix` Limanta - Hutama `Tefo`tuho Hulu - `Jehian` Norman Saviero - `Kezia` Suhendra - `Rizki` Halasan - `Roselina` Pradjanata --
