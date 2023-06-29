DIR = ../matlib/matrix
acc = 1e-6
POINTS = 600
dPoint = 60
startPoint = 60
DefaultShift = 0
bigLambda = 1e4
startShift = 0
dShift = 10
endShift = 1000
SafevUnsafe = SafeVsUnsafe

all : Out.txt SafeVsUnsafe.svg nTimed.svg shiftTimed.svg


#Safe vs unsafe graph data
SafeVsUnsafeData : $(SafevUnsafe)ShiftUnsafeVanilla.data $(SafevUnsafe)ShiftVanilla.data $(SafevUnsafe)NVanilla.data $(SafevUnsafe)NUnsafeVanilla.data

$(SafevUnsafe)ShiftUnsafeVanilla.data : main.exe
	>$@
	for N in $$(seq 0 1 16); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:300 -timingTest:True -version:0 -shift:$$N -seed:100 -acc:1e-5);\
	done

$(SafevUnsafe)ShiftVanilla.data : main.exe
	>$@
	for N in $$(seq 0 1 16); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:300 -timingTest:True -version:1 -shift:$$N -seed:100 -acc:1e-5);\
	done

$(SafevUnsafe)NVanilla.data : main.exe
	>$@
	for N in $$(seq 60 60 600); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:$$N -timingTest:True -version:0 -shift:0 -acc:1e-3 -seed:100);\
	done

$(SafevUnsafe)NUnsafeVanilla.data : main.exe	
	>$@
	for N in $$(seq 60 60 600); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:$$N -timingTest:True -version:1 -shift:0 -acc:1e-3 -seed:100);\
	done


data : nVanilla.data nV2Recal10.data nV2Recal500.data nV3Recal1e-2.data nV3Recal1e-2.data nV3Recal1e-21.data shiftVanilla.data shiftV2Recal500.data shiftV3Recal1e-21.data shiftV3Recal1e-2.data shiftV2Recal10.data
#Tests with shifted matrix


shiftVanilla.data : main.exe
	>$@
	for N in $$(seq $(startShift) $(dShift) $(endShift)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:300 -timingTest:True -version:1 -shift:$$N -seed:100 -acc:$(acc));\
	done

shiftV2Recal10.data : main.exe
	>$@
	for N in $$(seq $(startShift) $(dShift) $(endShift)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:300 -timingTest:True -version:2 -shift:$$N -seed:100 -recalEvery:10 -acc:$(acc));\
	done
shiftV2Recal500.data : main.exe
	>$@
	for N in $$(seq $(startShift) $(dShift) $(endShift)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:300 -timingTest:True -version:2 -shift:$$N -seed:100 -recalEvery:500 -acc:$(acc));\
	done

shiftV3Recal1e-2.data : main.exe
	>$@
	for N in $$(seq $(startShift) $(dShift) $(endShift)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:300 -timingTest:True -verson:3 -shift:$$N -seed:100 -recalRelErr:1e-2 -acc:$(acc));\
	done

shiftV3Recal1e-21.data : main.exe
	>$@
	for N in $$(seq $(startShift) $(dShift) $(endShift)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:300 -timingTest:True -verson:3 -shift:$$N -seed:100 -recalRelErr:1e-21 -acc:$(acc));\
	done



nV3Recal1e-21.data : main.exe
	>$@
	for N in $$(seq $(startPoint) $(dPoint) $(POINTS)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:$$N -timingTest:True -version:3 -shift:$(DefaultShift) -recalRelErr:1e-21 -acc:$(acc) -seed:100);\
	done
	for N in $$(seq $(startPoint) $(dPoint) $(POINTS)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:$$N -timingTest:True -version:3 -shift:$(DefaultShift) -recalRelErr:1e-21 -acc:$(acc) -seed:621);\
	done

nV3Recal1e-2.data : main.exe
	>$@
	for N in $$(seq $(startPoint) $(dPoint) $(POINTS)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:$$N -timingTest:True -version:3 -shift:$(DefaultShift) -recalRelErr:1e-2 -acc:$(acc) -seed:100);\
	done
	for N in $$(seq $(startPoint) $(dPoint) $(POINTS)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:$$N -timingTest:True -version:3 -shift:$(DefaultShift) -recalRelErr:1e-2 -acc:$(acc) -seed:621);\
	done

nV2Recal500.data : main.exe
	>$@
	for N in $$(seq $(startPoint) $(dPoint) $(POINTS)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:$$N -timingTest:True -recalEvery:500 -version:2 -shift:$(DefaultShift) -acc:$(acc) -seed:100);\
	done
	for N in $$(seq $(startPoint) $(dPoint) $(POINTS)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:$$N -timingTest:True -recalEvery:500 -version:2 -shift:$(DefaultShift) -acc:$(acc) -seed:621);\
	done
nV2Recal10.data : main.exe
	>$@
	for N in $$(seq $(startPoint) $(dPoint) $(POINTS)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:$$N -timingTest:True -recalEvery:10 -version:2 -shift:$(DefaultShift) -acc:$(acc) -seed:100);\
	done
	for N in $$(seq $(startPoint) $(dPoint) $(POINTS)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:$$N -timingTest:True -recalEvery:10 -version:2 -shift:$(DefaultShift) -acc:$(acc) -seed:621);\
	done

nVanilla.data : main.exe
	>$@
	for N in $$(seq $(startPoint) $(dPoint) $(POINTS)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:$$N -timingTest:True -shift:$(DefaultShift) -acc:$(acc) -seed:100);\
	done
	for N in $$(seq $(startPoint) $(dPoint) $(POINTS)); do \
		(time --format "$$N %e" --output $@ --append\
		mono $< -n:$$N -timingTest:True -shift:$(DefaultShift) -acc:$(acc) -seed:621);\
	done

nTimed.svg : nTimed.gpi data
	gnuplot < $<

shiftTimed.svg : shiftTimed.gpi data
	gnuplot < $<

SafeVsUnsafe.svg : SafeVsUnsafe.gpi SafeVsUnsafeData
	gnuplot < $<
SafeVsUnsafe.gpi :
	printf "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key bottom right \n\
	set title '300x300 matrix, accuracy goal 1e-5'\n\
	set multiplot layout 2,1 \n\
	set xlabel 'shift' \n\
	set ylabel 'Time taken (s)' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	plot '$(SafevUnsafe)ShiftVanilla.data' using (\$$1):(\$$2) index 0 with points pointtype 1 title 'Vanilla' \
	, '$(SafevUnsafe)ShiftUnsafeVanilla.data' using (\$$1):(\$$2) index 0 with points pointtype 4 title 'Unsafe Vanilla' \n\
	set title 'NxN matrix with a shift of 0, accuracy goal 1e-3'\n\
	set xlabel 'N'\n\
	plot '$(SafevUnsafe)NVanilla.data' using (\$$1):(\$$2) index 0 with points pointtype 1 title 'Vanilla' \
	, '$(SafevUnsafe)NUnsafeVanilla.data' using (\$$1):(\$$2) index 0 with points pointtype 4 title 'Unsafe Vanilla' \
	" >$@


shiftTimed.gpi :
	printf "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key top right \n\
	set xlabel 'shift' \n\
	set ylabel 'Computation Time (s)' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	set title '300x300 matrix, accuracy goal $(acc)'\n\
	plot 'shiftVanilla.data' using (\$$1):(\$$2) index 0 with points pointtype 1 title 'Vanilla' \
	, 'shiftV2Recal500.data' using (\$$1):(\$$2) index 0 with points pointtype 2 title 'V2 Recal Every 500' \
	, 'shiftV2Recal10.data' using (\$$1):(\$$2) index 0 with points pointtype 2 title 'V2 Recal Every 10' \
	, 'shiftV3Recal1e-21.data' using (\$$1):(\$$2) index 0 with points pointtype 3 title 'V3 recalErr 1e-21' \
	, 'shiftV3Recal1e-2.data' using (\$$1):(\$$2) index 0 with points pointtype 3 title 'V3 recalErr 1e-2' \
	" >$@

nTimed.gpi :
	printf "set terminal svg \n\
	set output '$(patsubst %.gpi,%.svg,$@)' \n\
	set key top right \n\
	set xlabel 'N' \n\
	set title 'NxN matrix, shift =$(DefaultShift) accuracy goal $(acc)' \n\
	set ylabel 'Computation Time (s)' \n\
	set xzeroaxis \n\
	set yzeroaxis \n\
	set samples 1600 \n\
	plot 'nVanilla.data' using (\$$1):(\$$2) index 0 with points pointtype 1 title 'Vanilla' \
	, 'nV2Recal10.data' using (\$$1):(\$$2) index 0 with points pointtype 2 title 'V2 recal every 10' \
	, 'nV2Recal500.data' using (\$$1):(\$$2) index 0 with points pointtype 2 title 'Improved, recal every 500' \
	, 'nV3Recal1e-21.data' using (\$$1):(\$$2) index 0 with points pointtype 3 title 'V3, recalErr = 1e-21' \
	, 'nV3Recal1e-2.data' using (\$$1):(\$$2) index 0 with points pointtype 3 title 'V3, recalErr = 1e-2' \
	" >$@

Out.txt : main.exe
	mono $< > $@

General.dll : $(DIR)/matrix.cs $(DIR)/vector.cs $(DIR)/../lineq/QRGS.cs power.cs
	mcs -target:library -out:$@ $^

main.exe: main.cs General.dll
	mcs -target:exe -out:main.exe $(addprefix -reference:,$(filter %.dll,$^)) $<
updateSVGs :
	$(RM) *.svg *.gpi

clean:
	$(RM) *.exe *.dll *log [Oo]ut* *.data *.svg *.par *.gpi
