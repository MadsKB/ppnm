Out.txt: main.exe
	mono main.exe > Out.txt

main.exe: main.cs
	mcs -target:exe -out:main.exe main.cs
test:
	mcs --version
clean:
	rm --force main.exe Out.txt
