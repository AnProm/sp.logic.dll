# sp.logic.dll
DLL for work with data and syntactic constructions
ГИТХАБ СИЛА
АНДРЕЙ КОДЕРСКАЯ БАН МАШИНА
#осторожно, снизу старые тесты
            
            Console.WriteLine(SyntacticConstructions.getResult("(byte in {10, 33} ) { }", 0)); 
            Console.WriteLine(SyntacticConstructions.getResult("foreach (byte in { 10, 33} ) { }",0));
            Console.WriteLine(SyntacticConstructions.getResult("foreach ( byte in { 10, 33 } ) { }", 0));
            Console.WriteLine(SyntacticConstructions.getResult("foreach (rar in { 10, 33} ) { }", 0));
            Console.WriteLine(SyntacticConstructions.getResult("foreach (byte in { 10, 33, sss} ) { }", 0));
            Console.WriteLine(SyntacticConstructions.getResult("foreach (byte in { SSSS }", 0));
            
            Console.WriteLine(SyntacticConstructions.getResult("if ( true == true ) { DEISTVIE }", 1));//выполнилось 1
            Console.WriteLine(SyntacticConstructions.getResult("if ( false == true ) { DEISTVIE }", 1));//не выполнилось ничего
            Console.WriteLine(SyntacticConstructions.getResult("if ( true == true ) { DEISTVIE } else { DEISTVIE2 }", 1));//1 выполнилось
            Console.WriteLine(SyntacticConstructions.getResult("if ( true != true ) { DEISTVIE } else { DEISTVIE2 }", 1));//2 выполнилось
            Console.WriteLine(SyntacticConstructions.getResult("if ( false == true ) { DEISTVIE } else { DEISTVIE2 }", 1));//2 условие выполнилось
            Console.WriteLine(SyntacticConstructions.getResult("if ( 7 == 7 ) { DEISTVIE } else { DEISTVIE2 }", 1));//1 условие выполнилось
            Console.WriteLine(SyntacticConstructions.getResult("if ( 7 == 8 ) { DEISTVIE } else { DEISTVIE2 }", 1));//2 условие выполнилось
            Console.WriteLine(SyntacticConstructions.getResult("if ( 7 == errorValue ) { DEISTVIE } else { DEISTVIE2 }", 1));//errorValue
            Console.WriteLine(SyntacticConstructions.getResult("if ( 7 < 8 ) { DEISTVIE } else { DEISTVIE2 }", 1));//проверка тернарного
            Console.WriteLine(SyntacticConstructions.getResult("if ( 8 < 7 ) { DEISTVIE } else { DEISTVIE2 }", 1));//проверка тернарного
            Console.WriteLine(SyntacticConstructions.getResult("if ( 8,22 > 7,2 ) { DEISTVIE } else { DEISTVIE2 }", 1));//doubleTest
            Console.WriteLine(SyntacticConstructions.getResult("if ( 8,22 <= 7,2 ) { DEISTVIE } else { DEISTVIE2 }", 1));//doubleTest
            Console.WriteLine(SyntacticConstructions.getResult("if ( arr2 == rrrar ) { DEISTVIE } else { DEISTVIE2 }", 1));//stringTest
            Console.WriteLine(SyntacticConstructions.getResult("if ( arr2 != rrrar ) { DEISTVIE } else { DEISTVIE2 }", 1));//stringTest
            Console.WriteLine(SyntacticConstructions.getResult("if ( arr2 != arr2 ) { DEISTVIE } else { DEISTVIE2 }", 1));//stringTest
            Console.WriteLine(SyntacticConstructions.getResult("if ( arr2 == arr2 ) { DEISTVIE } else { DEISTVIE2 }", 1));//stringTest
