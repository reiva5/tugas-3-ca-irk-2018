# Tugas 3 Ca-IRK 2018

## Disclaimer
Numbering tiles belong to [Sukma Sekararum](https://www.instagram.com/sekararums/)

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

-- `Catherine` Almira - `Dewita` Sonya Tarabunga - `Erick` Wijaya - `Felix` Limanta - Hutama `Tefo`tuho Hulu - `Jehian` Norman Saviero - `Kezia` Suhendra - `Rizki` Halasan - `Roselina` Pradjanata --
