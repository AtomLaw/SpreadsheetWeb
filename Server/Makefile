all: server

server: main.cc
	g++ -o $@ $? -lboost_system -lboost_thread-mt

run: server
	@echo "Running Server..."
	./server

test: tester.cc spreadsheet.cc
	g++ -o $@ $? -lboost_system -lboost_thread-mt -lboost_filesystem
clean:
	rm -f ./server