#Dentro de las cadenas de strings se manera de dos formas 
#como es con comillas dobles "" y las comillas simples''
my_string = "Mi primera String" #En esta linea esta hecha con commilas dobles
my_other_string = 'Mi segunda cadena de string' #Esta con comillas simples
#Conteo de una cadena de strings 
print(len(my_string))
print(len(my_other_string))
#Unir las dos cadenas de strings
print(my_string + " "+my_other_string)
#Para los saltos de lineas dentro de python lo hacemos de la sigueinte manera 
my_new_line_string = "Este es una string \nCon un salto de Linea"
print(my_new_line_string) 
#Tambien tenemos el salto de linea con tabulación
my_tab_string = "\tEste cadena de texto esta con Tabulación"
print(my_tab_string)
#TAMBIEN tenemos los strings con escapado de la siguiente manera
my_scape_string = "\\tEste es un tipo de String \\nEscapado"
print(my_scape_string)
#Tambien tenemos los de tipo de formateo es decir
name, surname, age = 'Stalin', 'Vega', 23
#Se lo puede formatear una cadena de texto con un % es decir:
print("Mi nombre es: {} {} y tengo la sigueinte edad {}".format(name, surname, age))
print("Mi nombre es: %s %s y tengo la sigueinte edad %d"%(name, surname, age))
#En el caso de usar la letra s es para cadenas de texto.
#La letra d es para numeros enteros
#La letra f es para flotantes
#Cuando este en la forma %.number of digital: para para numeros flotantes con presicion 
#Tambien esxiste la otra menera pero no es aconsejable 
print("Mi nomnbre es: "+name+" "+surname+"\nTengo la edad de: " +str(age))
#Tambien tenemos la inferencia de los datos donde se usa la letra f para darle mejor formato
print(f"Mi nombre es: {name} {surname} y tengo la siguiente edad {age}")
#Dentro de python tenemos el desenpaquetado de los caracteres
language = "Python"#La palabra se imprime segun las variables que se le da
language_1="python"
#Es decir que como python tiene 5 letras necesitamos 5 letras ejemplo a=p, b=y, y asi sucesivamente
a,b,c,d,e,f = language
print(a)
print(b)
print(e)
#Tambien tenemos la divison de los caracteres por ejemplo
language_slice = language[1:3]
print(language_slice)
#Dentro de esta operacion la cadena de string que es Python
#El resultado es yt que toma las posiciones entre P--hon
#Otro ejemplo que podemos ver es el siguiente
language_slice = language[1:]
print(language_slice)

language_slice = language[-2]
print(language_slice)

language_slice = language[1:2:4]
print(language_slice)

language_slice =language[0:6:2]
print(language_slice)

#Tambien podemos dar la vuelta es decir en reversa
reversed_language = language[::-1]
print(reversed_language)
#Tambien tenemos los metodos y las funciones 
#por ejmeplo
print(language_1.capitalize())#En esta funcion imprime la primera letra en mayuscula
print(language_1.upper())#Esta imprime la cadena de los caracteres todo en mayuscula
print(language_1.count("t"))#Imprime de la cadena el numero de la letra inicada en la operación
print(language_1.isnumeric())#Imprime en bool 
print("1".isnumeric())
print(language_1.lower())#Imorime en minuscula
print(language_1.lower().isupper())
print(language_1.startswith("py"))#Toma los primeros caracteres de la funcion para ver si la palabra es la correcta
print("py" == "py")


