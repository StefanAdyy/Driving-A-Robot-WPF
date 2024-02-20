# Driving-A-Robot-WPF

Aplicatia este de tip WPF application - in interiorul aplicatie sunt definite 2 zone astfel:
1. O zona superioara care ocupa 80% din suprafata aplicatiei si care se comporta ca o consola: in interiorul ei se scrie text linie cu line; textul este trimis catre aplicatie cand se apasa tasta ENTER moment in care comanda este executata si textul urca un rand in sus in consola. Astfel linia curenta se afla mereu pe cel mai de jos rand si consola se umple de jos in sus. Textele care nu sunt comenzi valide vor fi marcate fie cu culoare rosie fie folosind strikethrough peste text.
2. O zona inferioara ca o banda de afisaj in care sunt afisate mesajele de eroare sau output pentru comenzile de tip PRINT. Mesajele sunt prezentate pe aceasta banda atat timp cand nu s-a executat o noua comanda. O noua comanda care nu arunca o eroare sau un mesaj ca in cazul comenzii print o sa trimita catre banda un string empty astfel incat banda este goala.

Se da un robot care se poate misca in 3D, pe cele 3 axe: X, Y si Z.

Limitele inferioare si superioare ale celor 3 axe se citesc dintr-un fisier text de forma:
X -100 600
Y -50 1000
Z 0 300

Se cere dezvoltarea unei aplicatii care sa citeasca oricare dintre urmatoarele instructiuni (instructiunile se citesc de la tastatura, consecutiv) intr-o ordine aleatoare considerata de catre operator:
COMANDA         DESCRIERE
SET x, y, z     robotul se pozitioneaza pe pozitia x pe axa X, y pe axa Y si z pe axa Z
MOVE x, X       robotul inainteaza x unitati pe axa X
MOVE y, Y       robotul inainteaza y unitati pe axa Y
MOVE z, Z       robotul inainteaza z unitati pe axa Z
3DMOVE x, y, z  robotul inainteaza x unitati pe axa X, y unitati pe axa Y si z unitati pe axa Z

PRINT este afisata pozitia curenta a robotului pe cele 3 axe
RESET robotul se repozitioneaza la ultima locatie de tip x, y, z specificata de o comanda SET
QUIT aplicatia se inchide

Constrangeri:
- Robotul nu trebuie sa iasa in afara limitelor setate. Asta include si plasarea initiala a robotului. In cazul in care o instructiune duce la iesirea in afara limitelor, acea instructiune nu este luata in calcul si un mesaj de eroare apare
- Fiecare program trebuie sa inceapa cu o instructiune de SET. Nicio instructiune introdusa inainte de instructiunea SET nu va fi luata in calcul, ci va aparea un mesaj de eroare, cu urmatoarele exceptii:
- instructiunea QUIT - care inchide aplicatia
- instructiunea PRINT - care afiseaza in acest caz: ???, ???, ???
- Daca o instructiune nu este corecta (nu se gaseste in lista de instructiuni de mai sus), un mesaj de eroare apare
- Toate mesajele de eroare sunt salvate intr-un fisier, cu data si ora la care au fost intalnite

Exemplu de program cu limitele:
X -100 600
Y -50 1000
Z 0 300
SET 0, 0, 0
MOVE 10, X
MOVE 100, Y
PRINT
se afiseaza 10, 100, 0
MOVE 200, Z
PRINT
se afiseaza 10, 100, 200
MOVE 200, Z - instructiune nu e luata in calcul si un mesaj de eroare apare pe ecran,
mesaj care este salvat si intr-un fisier
mesaj de eroare afisat
MOVE 10, Z
PRINT
se afiseaza 10, 100, 210
SET 10, 10, 10
PRINT
se afiseaza 10, 10, 10
QUIT
