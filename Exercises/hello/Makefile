default: test-shell out.txt
out.txt : main.exe
	mono main.exe > out.txt

main.exe : hello.cs
	mcs hello.cs -out:main.exe -target:exe

clean:
	rm -f out.txt main.exe

test-shell:
	echo $$0
