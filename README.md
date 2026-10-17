# Statek – demo mechaniki statku w Unity

Małe demo mechaniki sterowania statkiem, zrobione w **Unity 6.5** (`6000.5.11f1`)
z wykorzystaniem **Input System** i **UI Toolkit**. Gracz steruje kadłubem łodzi oraz
masztem z żaglem, a prędkość łodzi zależy od tego, jak ustawi żagiel względem
zmiennego wiatru oraz od stopnia otwarcia żagla.

## Możliwości

- **Pływanie łodzią** – łódź płynie do przodu, a jej prędkość płynnie rośnie lub spada w zależności od wiatru i żagla.
- **Skręcanie z bezwładnością** – wciaskanie klawiszy od skręcania stopniowo zwiększa prędkość
  obrotu (jest limit), symulując obracanie sterem. Skręcając np. w prawo przez chwilę, po puszczeniu klawisza prędkośc obrotu zostaje i trzeba zacząć skręcać w lewo, aby wyzerować obrót.
- **Obrót masztu** – maszt z żaglem obraca się w zakresie od -45° do 45°.
- **Podnoszenie i opuszczanie żagla** – dolna reja przesuwa się w górę i w dół, a płótno
  żagla skaluje się razem z nią. Im bardziej rozwinięty żagiel, tym szybciej płynie łódź
  (żagiel zwinięty = 0% prędkości).
- **Dynamiczny wiatr** – kierunek wiatru losuje się co określony czas (w scenie co 20 s).
- **Wpływ wiatru na prędkość** – prędkość zależy od kąta między żaglem a wiatrem:

  | Ustawienie żagla względem wiatru | Mnożnik prędkości |
  |---|---|
  | prawie idealnie z wiatrem (dot ≥ 0.9) | ×2.0 |
  | dobrze z wiatrem (dot ≥ 0.7) | ×1.6 |
  | częściowo z wiatrem (dot ≥ 0.3) | ×1.3 |
  | w poprzek wiatru (dot ≥ -0.3) | ×1.0 |
  | częściowo pod wiatr (dot ≥ -0.7) | ×0.8 |
  | pod wiatr (dot ≥ -0.9) | ×0.65 |

  Progi i mnożniki można zmieniać w inspektorze komponentu `MastManager` (lista *Wind Boosts*).
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