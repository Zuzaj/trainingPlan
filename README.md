
# Dokumentacja Projektu - trainingPlan

## Wprowadzenie
Projekt `trainingPlan` to aplikacja internetowa stworzona w ASP.NET Core MVC, której celem jest zarządzanie planami treningowymi użytkowników. Użytkownicy mogą tworzyć, edytować oraz usuwać treningi. Następnie stworzone (lub wcześniej gotowe) treningi mogą dodawać do wybranego planu treningowego, ustawiając przy tym także inne parametry dotyczące planu. Można go również edytować, zobczyć szczegóły, a także (domyślnie po wykonaniu planu) usunąć.
Dodatkową funkcjonalnością jest filtrowanie treningów pod względem trudności oraz typu.
## Instalacja
1. Sklonuj repozytorium na swój lokalny komputer:
   ```sh
   git clone https://github.com/twoj-repozytorium/trainingPlan.git
2. Otwórz repozytorium w wybranym edytorze kodu (Visual Studio, Visual Studio Code).
3. Następnie do terminala wpisz następujące polecenia, aby przejść do docelowego folderu:
   ```sh
   cd trainingPlan
4. Uruchom aplikację:
   ```sh
   dotnet run
     
## Funkcjonalności
### Użytkownicy
- Rejestracja nowych użytkowników
- Logowanie użytkowników
- Wylogowywanie użytkowników
### Plany Treningowe
- Tworzenie nowych planów treningowych
- Edytowanie istniejących planów
- Usuwanie planów
- Przypisywanie treningów do planów
### Treningi
- Tworzenie nowych treningów
- Edytowanie istniejących treningów
- Usuwanie
- Filtrowanie treningów po trudności
- Filtrowanie treningów po typie

## Przykład użycia 
Podczas pierwszego uruchomienia aplikacji tworzy się domyślny użytkownik administratora. Dane logowania:
- Username: **admin**
- Password: **admin123**

  
Admin ma możliwość dodawania użytkowników, zarejestruj nowe konto.
Po zalogowaniu rzejdź do zakładki "YourPlan". Kliknij przycisk "Create New Plan". Wypełnij formularz (WeekStart, Commennts), wybierz interesujące Cię treningi i zapisz zmiany. Stworzony plan możesz edytować przyciskiem "Edit" lub podejrzeć szczegóły poprzez przycis "Details".
