CREATE TABLE IF NOT EXISTS pariwisata(
	id_tiket int,
	nama_pengunjung varchar(255),
	nama_tempat varchar(255),
	jumlah_tiket varchar(255),
	biaya varchar(255),
	PRIMARY KEY (id_tiket)
)
select * from pariwisata
INSERT INTO pariwisata(id_tiket, nama_pengunjung, nama_tempat, jumlah_tiket, biaya)
VALUES
(1,'Fizi','Danau Ranuagung','2','10000')