all: server

server: main.cc
	g++ -o $@ $? -lboost_system -lboost_thread-mt

run: server
	@echo "Running Server..."
	./server
clean:
	rm -f ./server