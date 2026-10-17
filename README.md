# Statek – demo mechaniki łodzi w Unity

Małe demo mechaniki sterowania łodzią, zrobione w **Unity 6.5** (`6000.5.11f1`)
z wykorzystaniem **Input System** i **UI Toolkit**. Gracz steruje łodzią oraz
masztem z żaglem, a prędkość łodzi zależy od tego, jak ustawi żagiel względem
zmiennego wiatru oraz od stopnia otwarcia żagla.

## Możliwości

- **Pływanie łodzią** – łódź płynie do przodu, a jej prędkość płynnie rośnie lub spada w zależności od wiatru i żagla.
- **Skręcanie z bezwładnością** – wciaskanie klawiszy od skręcania stopniowo zwiększa prędkość
  obrotu (jest limit), symulując obracanie sterem. Skręcając np. w prawo przez chwilę, po puszczeniu klawisza prędkośc obrotu zostaje i trzeba zacząć skręcać w lewo, aby wyzerować obrót i zacząć skręcać w drugą stronę.
- **Obrót masztu** – maszt z żaglem obraca się w zakresie od -45° do 45°.
- **Podnoszenie i opuszczanie żagla** – dolna reja przesuwa się w górę i w dół, a płótno
  żagla skaluje się razem z nią. Im bardziej rozwinięty żagiel, tym szybciej płynie łódź
  (żagiel zwinięty = 0% prędkości).
- **Dynamiczny wiatr** – kierunek wiatru losuje się co określony czas (w scenie co 20 s).
- **Wpływ wiatru na prędkość** – prędkość zależy od kąta między żaglem a wiatrem. Modyfikator prędkości zależny od wiatru działa na zasadzie progów - jest kilka zakresów kątu, i każdy z nich ustawia inną wartość mnożnika.
- **UI** – interfejs pokazuje stopień rozwinięcia żagla (%) i kąt obrotu masztu (°)
  oraz wskaźnik kierunku wiatru.
- **Obsługa kursora** – kursor jest ukryty i zamknięty w oknie gry; można go uwolnić klikając `Esc`
  i ponownie zablokować klikając lewym przyciskiem myszy w ekran.

## Sterowanie

| Klawisz | Akcja |
|---|---|
| **Q** | skręt łodzi w lewo |
| **E** | skręt łodzi w prawo |
| **A** | obrót masztu w lewo |
| **D** | obrót masztu w prawo |
| **W** | zwijanie (podnoszenie) żagla |
| **S** | rozwijanie (opuszczanie) żagla |
| **Esc** | odblokowanie i pokazanie kursora |
| **Lewy przycisk myszy** | ponowne ukrycie i zablokowanie kursora |