km-2013-projects-team-troglav
=============================
Knowledge Managament / 2013 / Projects / Team Troglav


Univerzitet "Džemal Bijedić" u Mostaru
Fakultet informacijskih tehnologija

Akademska godina: 2012./2013.

II Ciklus studija ::Godina studija: I

Predmet: Upravljanje znanjem

Predmetni profesor: prof.dr Vanja Bevanda 

Asistent: Adem Šabić    

Studenti: Edin Delanović-IM120027, Elvis Kadić-IM120017 

Seminarski rad iz praktičnog dijela predmeta

=================================================================================================================
 Naziv aplikacije: TRIGLAV
=================================================================================================================




o Aplikaciji 
********************


Triglav je web aplikacija sastavljena od dva modula FIT-Wiki & FIT-QA, sa integriranim algoritnom pretraživanja lucene.net i sistemom preporuke.

Prvi modul FIT-Wiki je baziran na ideji poznate wikipedia-e, dok je drugi modul FIT-QA baziran na ideji stranice stackoverflow. Oba modula imaju slične funkcionalnosti kao i originalne stranice.

Dovoljno je da u polje za pretragu unesete pojam i dobit će te listu članaka, pitanja ili autora koji bi mogli pomoći u vezi pojma vaše pretrage. U rezultate je uključena i originalna wikipedia kao dodatni/eksterni izvor 
informacija. 

Također, za registrirane korisnike omogućeno je glasanje za post(članak/pitanje)na skali od 1 do 5 i tzv. "lajkanje".
Svaki korisnik i post je uvršten u sistem bodovanja i rangiranja. U zavisnosti od broja bodova zarađenim na post-u 
određena su pravila za dobivanje bronaznih, srebrenih i zlatnih medalja za korisnika. U zavisnosti od ukupnog broja
prikupljenih medalja korisniku se dodijeljuje odgovarajući bedž koji asocira na njegov rejting. 

Pokretanjem aplikacije moći ćete isprobati ostale funkcionalnosti.



Korišteni softver 
********************
Microsoft Visual Studio 2012 - Student Version
Microsoft SQL Server 2012 - Student Version 
Adobe Photoshop CS6



                                                                                      
Uputstvo za pokretanje Aplikacije 
************************************

Postoji više načina pokretanja ove aplikacije. U slijedećem tekstu će biti objašnjen najjednostavniji.

1.KORAK
Potrebno uraditi dovnload izvornih fajlova smeštenih u .zip datoteku, sa GitHub Stranice.

2.KORAK 
Sadržaj .zip datoteke smjestiti u folder pod imenom Triglav.

3.KORAK
Potrebno je obezbijediti disk koji ima drive letter J:\(to može biti memory stick, eksterni 
hard disk, ili nova particija). Struktura direktorija na kraju bi trebala izgledati: "J:\Triglav_Web_App\Triglav"
(U folderu Triglav bi trebao biti sadržaj respakovane .zip datoteke)

4.KORAK
U Sql Serveru potrebno je uraditi attach baze podataka koja se nalazi u folderu "J:\Triglav_Web_App\Triglav\DB"
Baza je lokalnog karaktera tako da nema potrebe mijenjati connection string u web.config-u.

5.KORAK
Pokrenuti dokument Triglav.sln (Microsoft Visual Studio Solution)

6.KORAK
Nakon što ste otvorili dokument, u solution exploreru(Visual Studio) potrebno je otvoriti projekt koji ima naziv Web.
Nakon toga otvoriti folder Public, te dvostrukim klikom kliknuti na page Home.aspx

7.KORAK
Ako ste uspješno uradili predhodne korake i nalazite se na Home.aspx stranici, ostalo je samo da kompajlirate
program i isprobate funkcionalnosti aplikacije.

8.KORAK(opcionalno)
Da bi koristili sve funkcionalnosti aplikacije korisnici se moraju registrovati.
Ako ipak želite preskočiti taj korak, možete se logirati kao postojeći korisnik sa slijedećim podacima:
Korisnik: blocky, kor01; kor02; kor03; kor04; kor05; kor06; kor07; kor08; kor09; kor10; kor11; kor12; kor13; kor14; 
Lozinka: test (za sve gore navedene usere)



____________________________________________________________________________________________________________________
                                                                                                  @copyright by: FIT

