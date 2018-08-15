# Tugas-3-Ca-IRK
Repository Git untuk tugas 3 ca-IRK

## Pembuatan Aplikasi
Tugas ini berisi aplikasi game Minesweeper di Windows(tidak dapat dibuka di Linux) yang saya buat sendiri. Untuk membuat game ini, saya menggunakan Python versi 3.6 dengan tambahan library Tkinter(sudah terpasang secara otomatis dalam Pythonnya, dengan link: https://www.python.org) dan Pillow versi 5.2(library tambahan yang harus diinstall sendiri, melalui tulisan pip install pillow di Command Prompt). Untuk pembuatan executable file, saya mengikuti tutorial sebagai berikut: https://medium.com/dreamcatcher-its-blog/making-an-stand-alone-executable-from-a-python-script-using-pyinstaller-d1df9170e263, yaitu menginstall pyinstaller dengan mengetik : pip install pyinstaller di Command Prompt dan PyWin32 dengan mengunduh Release 223 di https://github.com/mhammond/pywin32/releases, atau sederhananya langsung memasukkan link berikut di search barnya: https://github.com/mhammond/pywin32/releases/download/b223/pywin32-223.win32-py3.6.exe.

## Pengujian
Untuk pengujian, tidak perlu menginstall tambahan library yang disebutkan di atas karena sudah dalam bentuk aplikasi yang dapat langsung dieksekusi.(Jika ternyata tidak dapat dieksekusi, yang perlu diunduh hanya Python 3.6 dan Pillow saja) Untuk versi 3, logo.png dan Minesweeper.py harus diletakkan dalam satu folder yang sama. Pengujian dapat dilakukan dengan double-click pada aplikasi.

## Versi
Terdapat 3 versi game ini

### Versi Pertama
Untuk versi ini, minesweeper dapat berbentuk persegi panjang (ukuran baris dan kolom tidak sama), jumlah mines yang ditentukan pengguna belum ada, dan input koordinat dari pengguna bukan berbentuk <nilai_absis> <nilai_koordinat>, melainkan (nilai_absis, nilai_koordinat).

### Versi Kedua
Untuk versi ini, ketiga spesifikasi tersebut sudah disesuaikan dengan spek yang diberikan, dan pemberian koordinat dalam <nilai_absis> <nilai_koordinat> (ada spasinya)

### Versi Ketiga
Untuk versi ini, sudah terdapat GUI dengan Tkinter dan Pillow, dengan tombol "New Game" akan masuk ke command prompt layaknya versi kedua, tombol "How to Play" yang berisi tujuan bermain Minesweeper dengan singkat(diasumsikan pengguna sudah tahu cara memainkan game Minesweeper), dan tombol "Quit" yang akan mengeluarkan pengguna dari aplikasi. Di versi ini terdapat pewaktu sehingga dapat diketahui waktu penyelesaian Minesweeper. Setiap kali pengguna menyelesaikan suatu game, aplikasi akan membawa ke awal sampai pengguna menutup aplikasi tersebut(dengan menekan tombol silang di kanan atas maupun melalui tombol "Quit"). Setiap kali penekanan suatu tombol, jendela akan menutup sehingga tidak dapat kembali ke menu awal(kecuali menyelesaikan permainan setelah menekan tombol "New Game" atau "Back to Menu" setelah menekan tombol "How to Play"). Ukuran jendela tidak dapat berubah.

### Tambahan
Untuk setiap versi, terdapat versi .py juga yang ditujukan untuk melihat kode yang digunakan sehingga membentuk aplikasi tersebut.

## Hal-hal yang Perlu Diperhatikan
Ukuran board minimal adalah 8 x 8 dan memiliki maksimal berupa nilai maksimal dari bilangan bulat di Python. Ukuran tidak boleh diinput angka selain bilangan bulat(tidak ada pengecekan bilangan float). Terdapat minimal 4 jenis level yang bergantung pada ukuran board, yaitu easy, medium, hard, dan insane. Berikut adalah simbol-simbol yang digunakan dalam game ini:
1. Angka 1-8 menunjukkan jumlah mines yang berada di sekitar cell tersebut.
2. Karakter ' _ ' menunjukkan cell yang kosong atau tidak ada mines di sekitar cell tersebut.
3. Karakter ' F ' menunjukkan cell yang di-flag.
4. Karakter ' X ' menunjukkan cell yang masih belum dibuka atau di-flag.
5. Tulisan "BOOM" yang berarti pengguna sudah membuka cell yang berisi mine sehingga dinyatakan kalah saat itu juga.
