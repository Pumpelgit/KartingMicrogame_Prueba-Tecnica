# Karting Microgame

## Nombre
Karting Microgame

## Descripción
Template de "Karting Microgame" modificado para cumplir los requisitos de la prueba técnica.

La escena de *MainMenu* se ha modificado para ajustarse a un diseño *responsive* de distintas pantallas en landscape y para usar el patrón *MVC* para la UI. Tambien se ve el mejor tiempo y monedas acumuladas como pedía la prueba.

He procurado mantener la misma estructura y patrones de diseño que el proyecto original, principalmente en la escena de la carrera *MainScene* para adaptarme al proyecto. Se han añadido monedas por todo el recorrido para que el jugador recoja. En la parte superior derecha se ven la cantidad de monedas que ha recolectado el jugador durante la carrera. Al dar una vuelta si el tiempo ha mejorado respecto al mejor tiempo conseguido hasta ahora se mostrará en la zona central usando el sistema de notificaciones que ya había en el proyecto.

En la barra superior de menus de Unity en el apartado de *Karting* encontramos una opción de *Quick Settings Menu*. Si hacemos click aparece una ventana donde el jugador podrá seleccionar una configuración predeterminada para editar las vueltas que tiene que dar el jugador, la velocidad máxima y la aceleración. Esto es una muestra de lo que se pedía para facilitar la labor de un diseñador.

## Controles
Al mantener el dedo tocando la pantalla saldrá un circulo que al moverlo moverá el Kart en la dirección que se elija.
IMPORTANTE: Si se va a probar el proyecto en Unity con las teclas de WASD que estaban puestas inicialmente hay que volver a añadir el script de KeyboardInput.cs al Kart y quitar el MobileInput.cs

## Instalación
Descargar el proyecto y abrir con la version 2021.3.10f1 de Unity.

## Modificaciones y cambios
La mayor parte de archivos nuevos están dentro de la carpeta de *LabCaveCustom*:
    
- **BestTimeController.cs**
        Se encuentra en la escena de *MainMenu* y se encarga de mostrar el tiempo más rápido guardado hasta el momento. Al activarse el script esta pendiente de *OnLoadData* de *SaveManager.cs* para recibir el tiempo una vez se haya cargado y lo pasa a *MenuElementView.cs* para que se muestre en pantalla.
    
- **GameSettingsDataSO.cs**
        Contiene el scriptable object que contiene la velocidad, número de vueltas y aceleración. *ArcadeKart.cs* y *ObjectiveCompleteLaps.cs* tienen la referencia a este SO para tener acceso a estos datos de configuración.
    
- **MainMenuController.cs**
        Se encuentra en la escena de *MainMenu* y es uno de los cambios pedidos para modificar la UI del juego usando otro patrón. En este caso se ha usado el patrón *MVC* siendo el controlador que activa y desactiva la UI de los controles y cambia la escena a la de *MainScene*.
    
- **MenuElementView.cs**
        Se encuentra en la escena de *MainMenu* y es la parte visual de mostrar el mejor tiempo y las monedas totales. Seria la parte View del patrón MVC.
    
- **MobileInput.cs**
        Se encarga del control de los inputs de movil para mover el Joystick de la pantalla. Hereda de BaseInput que venía ya implementado en el proyecto original. 
    
- **PlayerData.cs**
        El modelo de datos del jugador como el nombre, el tiempo y las monedas.
    
- **QuickSettingsSO.cs**
        Es el scriptable object de configuración que luego se asigna en el *GameSettingsDataSO.cs*. Se podrían crear varios achivos de configuración para diferentes niveles o configuraciones.
    
- **SaveManager.cs**
        Guarda y carga los datos del jugador. En cada escena hay un SaveManager (yo haría que fuera un Singleton) para que en la *MainMenu* se pueda cargar los datos del jugador y en la *MainScene* se puedan guardar los datos del jugador. Los datos de PlayerData se guardan en un archivo JSON en *PersistentDataPath*.
    
- **TotalCoinController.cs**
        Similar a *BestTimeController.cs* pero para las monedas.
    
- **CoinObject.cs**
        Hereda de *TargetObject.cs* que venía ya implementado en el proyecto original. Se ha modificado para que al colisionar con el kart se añada una moneda al jugador. Y actualice el display de monedas.
    
- **CoinRespawnManager.cs**
        Escucha el evento de *OnUpdateLap* de *ObjectiveCompleteLaps.cs* para que cuando se complete una vuelta se vuelvan a activar las monedas y cuando acabe todas desactive las monedas para que no pueda coger mas.
    
- **CoinDisplay.cs**
        Script similar al de *TimeDisplay.cs* pero para mostrar las monedas recogidas durante la carrera usando la misma estructura que ya había implementada para mostrar el tiempo.
    
- **QuickSettingsEditor.cs**
        En la barra superior de Unity he añadido un menu de *Karting/Quick Settings Window* para facilitar el labor de un posible diseñador para cambiar algunos elementos del juego como la velocidad, aceleración y/o vueltas que dar. Este da la posibilidad tambien de crear distintas configuraciones de juego.

Se han **modificado** los siguientes aspectos del proyecto original:

  - En la escena de *MainMenu* se ha modificado el *Rectransform* (anchor y dimensiones) para ajustarse a un diseño *responsive* de distintas pantallas en landscape.
    
- Previamente el boton de *Controls* desactivaba el modelo del Kart y activaba la Imagenes de los controles directamente mediante el evento de Unity de OnClick. Ahora se ha modificado para que el OnClick llame al anteriormente explicado *MainMenuController.cs* para que escale mejor.
    
- Previamente el boton de *Play* cambiaba la escena a la de *MainScene*. Estaba "hardcodeada" y solo cambiaba a esta escena. Ahora se pasa por parametro el nombre de la escena a la que se quiere cambiar para que escale mejor.
    
- Se ha añadido en la escena de *MainMenu* dentro del GameObject de *Canvas* *BestLapPanel* y *TotalCoinPanel* que muestran el mejor tiempo hasta el momento y el total de monedas acumuladas de todas las sesiones. Estos son parte del cambio de la interfaz que se ha pedido usando otro patrón. Aparte de esto tambien se ha añadido un el *SaveManager* para cargar los datos del jugador.
    
- **ObjectiveCompleteLaps.cs**
        Se ha modificado para que ahora se pueda acceder a *GameSettingsDataSO.cs*  y por lo tanto a la configuración seleccionada actual para obtener el número de vueltas que se quieren hacer.
        
```
    public GameSettingsDataSO gameSettingsData;
    private QuickSettingsSO quickSettings;

    quickSettings = gameSettingsData.GameSettings;s
    lapsToComplete = quickSettings.NumberOfLaps;
```
- **TimeDisplay.cs**
        Cuando se pide mostrar la mejor vuelta de la sesión actual compruebo si es mejor que la mejor vuelta. Si es así actualizo el tiempo en el *SaveManager.cs* para que se guarde y muestro en pantalla un mensaje de "Best Time" el centro de la pantalla usando el sistema ya implementado en el proyecto original de notificaciones.
        
```
	if(finishedLapTimes[bestLap] < bestTime || bestTime <=0f)
	{
	    bestTime= finishedLapTimes[bestLap];
	    recordBestTimeMessage.message = $"New Record : {getTimeString(bestTime)}";
	    recordBestTimeMessage.Display();
	    SaveManager.OnSaveBestTime(finishedLapTimes[bestLap]);

	}
```
- En GameManager > GameHUD de MainScene encontramos *CoinCanvas*. Este GameObject contiene el Canvas y la lógica que controla la cantidad de monedas que lleva el jugador durante la carrera.
    
- En GameManager, *RecordBestTimeMessage* se ha añadido para mostrar el mensaje de "Best Time" en la pantalla.
    
- El GameObject de *JoystickMobile* contiene el canvas e imagenes para la interfaz del joystick para cuando se juega en móvil.
    
- *CoinsToPickUp* GameObject contiene el *CoinRespawnManager.cs* que se encarga de activar y desactivar las monedas en la escena. Como hijos estan las monedas de la pista que se recogen.
    
- *SaveManager* contiene la lógica de guardar y cargar los datos del jugador. Por facilitar las cosas se guardan los datos cuando se destruye el GameObject es decir al cambiar de escena o cerrar el juego.
