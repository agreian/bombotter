INC = -I../inc -I../gen
LOCALINC = -I.
ICELIB = -lIce -lIceUtil -lpthread
CC = g++
OFILES = ../tmp/ServerApplication.o \
	../tmp/Game.o \
	../tmp/Bomberloutre_*.o \
	../tmp/UserConnectionI.o \
	../tmp/GamesManagerI.o

all: dir interfaces Bomberloutre.o ServerApplication.o Game.o UserConnectionI.o GamesManagerI.o
	cd ./src; \
	$(CC) $(INC) $(ICELIB) $(OFILES) main.cpp -o ../bin/main; \
	cd ..;

dir:
	mkdir -p ./tmp; \
	mkdir -p ./bin; \
	mkdir -p ./gen;

Bomberloutre.o:
	cd ./gen; \
	$(CC) $(LOCALINC) -c Bomberloutre.cpp -o ../tmp/Bomberloutre_main.o; \
	$(CC) $(LOCALINC) -c Bomberloutre_map.cpp -o ../tmp/Bomberloutre_map.o; \
	$(CC) $(LOCALINC) -c Bomberloutre_game.cpp -o ../tmp/Bomberloutre_game.o; \
	$(CC) $(LOCALINC) -c Bomberloutre_object.cpp -o ../tmp/Bomberloutre_object.o; \
	$(CC) $(LOCALINC) -c Bomberloutre_player.cpp -o ../tmp/Bomberloutre_player.o; \
	$(CC) $(LOCALINC) -c Bomberloutre_user.cpp -o ../tmp/Bomberloutre_user.o; \
	cd ..;

Game.o:
	cd ./src; \
	$(CC) $(INC) -c Game.cpp -o ../tmp/Game.o; \
	cd ..;

ServerApplication.o:
	cd ./src; \
	$(CC) $(INC) -c ServerApplication.cpp -o ../tmp/ServerApplication.o; \
	cd ..;

UserConnectionI.o:
	cd ./src; \
	$(CC) $(INC) -c UserConnectionI.cpp -o ../tmp/UserConnectionI.o; \
	cd ..;

GamesManagerI.o:
	cd ./src; \
	$(CC) $(INC) -c GamesManagerI.cpp -o ../tmp/GamesManagerI.o; \
	cd ..;

interfaces:
	cd ./interface; \
	slice2cpp $(LOCALINC) --header-ext hpp --output-dir ../gen Bomberloutre*.ice; \
	cd ..;

clean:
	rm -Rf ./tmp; \
	rm -Rf ./gen; \
	rm -Rf ./bin;
