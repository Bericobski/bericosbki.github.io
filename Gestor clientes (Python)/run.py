#El script que pone en marcha el programa 
import sys
import ui
import menu 

if __name__ == "__main__": 
    if len(sys.argv) > 1 and sys.argv[1] == "-t": #Esto es para entrar en el modo terminal, desde la terminal
        menu.iniciar()
    else: #Ejecutar sin argumento, ejecuta el modo grafico del programa
        app = ui.MainWindow()
        app.mainloop()
    

