
# Operación Fuego de Quasar

Han Solo ha sido recientemente nombrado General de la Alianza
Rebelde y busca dar un gran golpe contra el Imperio Galáctico para
reavivar la llama de la resistencia.
El servicio de inteligencia rebelde ha detectado un llamado de auxilio de
una nave portacarga imperial a la deriva en un campo de asteroides. El
manifiesto de la nave es ultra clasificado, pero se rumorea que
transporta raciones y armamento para una legión entera.

## Detalles de implementación

Para dar soporte desde el área técnica, contamos con un web service que recibe información de tres satellites.
Los satellites nos brindan la información sobre el mensaje que envía un emisor y la distancia que hay entre el emisor y el satellite.
Este mensaje puede llegar con interferencias a cada satelite, por eso debemos utilizar los tres disponibles para poder completar la información recibida.
Asimismo, utilizamos la distancia que nos arroja cada satelite para poder triangular la posición del emisor.

### Posición de los satélites actualmente en servicio
* Kenobi: [-500, -200]
* Skywalker: [100, -100]
* Sato: [500, 100]

## API rest V1.0

Para conocer más sobre la API vaya al siguiente [enlace](https://hansoloservice.azurewebsites.net/swagger/index.html)

### Consideraciones:
* La unidad de distancia en los parámetros de GetLocation es la misma que la que se utiliza para indicar la posición de cada satélite.
* Para determinar la posición del emisor del mensaje se utiliza un algoritmo de trilateración. Para mas información [https://www.101computing.net/cell-phone-trilateration-algorithm]
* El mensaje recibido en cada satélite se recibe en forma de arreglo de strings.
* Cuando una palabra del mensaje no pueda ser determinada, se reemplaza por un string en blanco en el array.
** Ejemplo: [“este”, “es”, “”, “mensaje”]
* Considerar que existe un desfasaje previo a comenzar a recibir el mensaje en cada satélite.
** Ejemplo: 
		Kenobi: [“”, “este”, “es”, “un”, “mensaje”]
		Skywalker: [“este”, “”, “un”, “mensaje”]
		Sato: [“”, ””, ”es”, ””, ”mensaje”]

## Compilar y Ejecutar

La API fue desarrollada en .Net Core 3.1.

Para poder compilar y ejecutar la aplicación se necesita disponer del runtime. [https://dotnet.microsoft.com/download/dotnet/3.1]

Luego ejecutar 

dotnet build

dotnet run

Esto levanta una web app en http://localhost:5000.

Para acceder al punto de entrada http://localhost:5000/swagger.

Para ejecutar los test

dotnet test
