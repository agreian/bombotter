INC = -I../inc
LOCALINC = -I.
ICELIB = -lIce -lIceUtil -lpthread
CC = g++
OFILES = ../tmp/ServerApplication.o \
	../tmp/Game.o \
	../tmp/Bomberloutre_*.o \
	../tmp/UserConnectionI.o

all: interfaces Bomberloutre.o ServerApplication.o Game.o UserConnectionI.o
	mkdir -p ./tmp; \
	cd ./src; \
	$(CC) $(INC) $(ICELIB) $(OFILES) main.cpp -o ../bin/main; \
	cd ..;

Bomberloutre.o:
	cd ./src; \
	$(CC) $(INC) -c Bomberloutre.cpp -o ../tmp/Bomberloutre_main.o; \
	$(CC) $(INC) -c Bomberloutre_map.cpp -o ../tmp/Bomberloutre_map.o; \
	$(CC) $(INC) -c Bomberloutre_game.cpp -o ../tmp/Bomberloutre_game.o; \
	$(CC) $(INC) -c Bomberloutre_object.cpp -o ../tmp/Bomberloutre_object.o; \
	$(CC) $(INC) -c Bomberloutre_player.cpp -o ../tmp/Bomberloutre_player.o; \
	$(CC) $(INC) -c Bomberloutre_user.cpp -o ../tmp/Bomberloutre_user.o; \
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

interfaces:
	cd ./interface; \
	slice2cpp $(LOCALINC) --header-ext hpp --output-dir ../src Bomberloutre*.ice; \
	mv ../src/Bomberloutre*.hpp ../inc/; \
	cd ..;

clean:
	rm -Rf ./tmp; \
	rm ./inc/Bomberloutre*.hpp; \
	rm ./src/Bomberloutre.cpp;
