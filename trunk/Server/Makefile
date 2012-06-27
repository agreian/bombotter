INC = -I../inc -I../gen
LOCALINC = -I.
ICELIB = -lIce -lIceUtil -lpthread
CC = g++
OFILES = ../tmp/ServerApplication.o \
	../tmp/Bomberloutre_*.o \
	../tmp/BomberServer.o

all: dir interfaces Bomberloutre ServerApplication BomberServer
	cd ./src; \
	$(CC) $(INC) $(ICELIB) $(OFILES) main.cpp -o ../bin/main; \
	cd ..;

dir:
	mkdir -p ./tmp; \
	mkdir -p ./bin; \
	mkdir -p ./gen;

Bomberloutre:
	cd ./gen; \
	$(CC) $(LOCALINC) -c Bomberloutre.cpp -o ../tmp/Bomberloutre_main.o; \
	$(CC) $(LOCALINC) -c Bomberloutre_map_item.cpp -o ../tmp/Bomberloutre_map_item.o; \
	cd ..;

Bomberloutre.old:
	cd ./gen; \
	$(CC) $(LOCALINC) -c Bomberloutre.cpp -o ../tmp/Bomberloutre_main.o; \
	$(CC) $(LOCALINC) -c Bomberloutre_map.cpp -o ../tmp/Bomberloutre_map.o; \
	$(CC) $(LOCALINC) -c Bomberloutre_game.cpp -o ../tmp/Bomberloutre_game.o; \
	$(CC) $(LOCALINC) -c Bomberloutre_map_item.cpp -o ../tmp/Bomberloutre_map_item.o; \
	$(CC) $(LOCALINC) -c Bomberloutre_player.cpp -o ../tmp/Bomberloutre_player.o; \
	$(CC) $(LOCALINC) -c Bomberloutre_user.cpp -o ../tmp/Bomberloutre_user.o; \
	cd ..;

BomberServer:
	cd ./src; \
	$(CC) $(INC) -c BomberServer.cpp -o ../tmp/BomberServer.o;\
	cd ..;
	
ServerApplication:
	cd ./src; \
	$(CC) $(INC) -c ServerApplication.cpp -o ../tmp/ServerApplication.o; \
	cd ..;

interfaces:
	cd ./interface; \
	slice2cpp $(LOCALINC) --header-ext hpp --output-dir ../gen Bomberloutre*.ice; \
	cd ..;

clean:
	rm -Rf ./tmp; \
	rm -Rf ./gen; \
	rm -Rf ./bin;
