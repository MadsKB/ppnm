Out.txt: main.exe
	mono $< > $@

vec.dll : vec.cs
	mcs $< -target:library -out:$@ 

main.exe: main.cs vec.dll
	mcs -target:exe -out:$@ -reference:vec.dll main.cs

test:
	mcs --version
clean:
	rm --force main.exe Out.txt vec.dll
