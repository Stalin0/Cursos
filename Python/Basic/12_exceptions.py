#En este apartado vamos a ver las excepciones dentro de este fichero
#Tambien se lo llama manejo de errores
numberOne, numberTwo = 5, 5
numberTwo = "52"
#Esto es manejo de excepciones manualmente
if type(numberTwo) == int:
    print(numberOne +numberTwo)
else:
    print("Es falsa el bool")
#La manera mas formal try-except
try:
    print(numberOne + numberTwo)
    print("Ejecuta correctamente")
except:
    #Hace la ejecucion si en el caso el bool es false
    print("Es un error fatal")
#Exepciones con try-except-else
try:
    print(numberOne + numberTwo)
    print("Ejecuta correctamente")
except:
    print("Es un error fatal")
else:#Son maneras opcionales depende de la necesidad
    #Se ejecuta si no se produce una excepción
    print("La ejecución continuara")
finally:#Son maneras opcionales depende de la necesidad 
    print("Esta bien la ejecución")#Esto se imprime en los dos casos es decr siempre 
#Excepciones por tipo
try:
    print(numberOne +numberTwo)
    print("Pasa correctamente")
except ValueError:
    print("Error tipo ValueError")
except TypeError:
    print("Error typeError")
#Excepciones de captura de error
try:
    print(numberOne + numberTwo)
    print("Pasa correctamente")
except ValueError as error:
    print(error)
except Exception as exception:
    print(exception)