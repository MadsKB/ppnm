testy = This is a test echo.
default: test-shell Out.txt
Out.txt : main.exe
	mono main.exe > Out.txt

main.exe : hello.cs
	mcs hello.cs -out:main.exe -target:exe

clean:
	rm -f Out.txt main.exe

test-shell:
	echo $(testy)
