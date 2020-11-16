DROP DATABASE test;

CREATE DATABASE test CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;
Use test;

create table tai_khoan(
id_tk int AUTO_INCREMENT primary key,
ten_dang_nhap varchar(30) not null UNIQUE,
mat_khau varchar(30) not null,
ten_nhan_vat varchar(50) default ''
)ENGINE=InnoDB  DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci AUTO_INCREMENT=1;

insert into tai_khoan(ten_dang_nhap, mat_khau, ten_nhan_vat) values('admin1', '123456', 'admin1');
insert into tai_khoan(ten_dang_nhap, mat_khau) values('admin2', '123456');
insert into tai_khoan(ten_dang_nhap, mat_khau) values('admin3', '123456');

-- update tai_khoan set ten_nhan_vat = 'Phi123' Where id_tk = 2;

create table nhan_vat(
id_nv int AUTO_INCREMENT primary key,
id_cfg varchar(30) not null,
id_tk int not null,
lv int default 1,
idx int default -1,
foreign key(id_tk) references tai_khoan(id_tk)
)ENGINE=InnoDB  DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci AUTO_INCREMENT=1;

insert into nhan_vat(id_cfg, id_tk, lv, idx) values('T1000', 1, 1, 0);
insert into nhan_vat(id_cfg, id_tk, lv, idx) values('T1001', 1, 1, 1);
insert into nhan_vat(id_cfg, id_tk, lv, idx) values('T1002', 1, 1, 2);
insert into nhan_vat(id_cfg, id_tk, lv, idx) values('T1003', 1, 1, 3);
insert into nhan_vat(id_cfg, id_tk, lv) values('T1004', 1, 1);
insert into nhan_vat(id_cfg, id_tk, lv) values('T1005', 1, 1);
insert into nhan_vat(id_cfg, id_tk, lv) values('T1006', 1, 1);
insert into nhan_vat(id_cfg, id_tk, lv) values('T1003', 1, 1);
insert into nhan_vat(id_cfg, id_tk, lv) values('T1003', 1, 1);

-- update nhan_vat set idx = 4 Where id_nv = 5;
-- update nhan_vat set lv = 4 Where id_nv = 5;

-- SELECT * FROM nhan_vat where id_nv = 5 order by id_nv desc limit 1;

-- SELECT * FROM nhan_vat where id_tk = 1 and id_cfg = 'T1003' order by id_nv desc limit 1;

create table milestone(
id_ml int primary key
)ENGINE=InnoDB  DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

insert into milestone(id_ml) values(0);
insert into milestone(id_ml) values(1);
insert into milestone(id_ml) values(2);
insert into milestone(id_ml) values(3);
insert into milestone(id_ml) values(4);
insert into milestone(id_ml) values(5);
insert into milestone(id_ml) values(6);
insert into milestone(id_ml) values(7);
insert into milestone(id_ml) values(8);
insert into milestone(id_ml) values(9);
insert into milestone(id_ml) values(10);
insert into milestone(id_ml) values(11);
insert into milestone(id_ml) values(12);
insert into milestone(id_ml) values(13);
insert into milestone(id_ml) values(14);
insert into milestone(id_ml) values(15);
insert into milestone(id_ml) values(16);
insert into milestone(id_ml) values(17);
insert into milestone(id_ml) values(18);
insert into milestone(id_ml) values(19);
insert into milestone(id_ml) values(20);
insert into milestone(id_ml) values(21);
insert into milestone(id_ml) values(22);
insert into milestone(id_ml) values(23);
insert into milestone(id_ml) values(24);
insert into milestone(id_ml) values(25);
insert into milestone(id_ml) values(26);
insert into milestone(id_ml) values(27);
insert into milestone(id_ml) values(28);
insert into milestone(id_ml) values(29);

create table tick_milestone(
id_tml int AUTO_INCREMENT primary key,
id_tk int not null,
id_ml int not null,
star int not null default 0,
foreign key(id_ml) references milestone(id_ml),
foreign key(id_tk) references tai_khoan(id_tk)
)ENGINE=InnoDB  DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci AUTO_INCREMENT=1;

insert into tick_milestone(id_tk, id_ml, star) values(1, 0, 2);
insert into tick_milestone(id_tk, id_ml, star) values(1, 1, 3);
insert into tick_milestone(id_tk, id_ml, star) values(1, 2, 2);
insert into tick_milestone(id_tk, id_ml, star) values(1, 3, 1);
insert into tick_milestone(id_tk, id_ml, star) values(1, 4, 3);
insert into tick_milestone(id_tk, id_ml, star) values(1, 5, 2);
insert into tick_milestone(id_tk, id_ml, star) values(1, 6, 3);
insert into tick_milestone(id_tk, id_ml, star) values(1, 7, 1);

-- update tick_milestone set star = 3 Where id_tk = 1 and id_ml = 0;
SELECT * FROM tick_milestone where id_tk = 1 and id_ml = 0 order by id_tml desc limit 1;