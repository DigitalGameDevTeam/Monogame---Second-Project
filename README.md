# Monogame---Second-Project

Técnicas de Desenvolvimento de Jogos - 2ªFase:

Grupo : -> João Ribeiro // 27926 -> Martim Moreira // 22354 -> Gonçalo Veloso // 22348

Índice:
1 - Introdução	
2 - Movimentos e ações	
3 - Interface	
4 - Arte	
5 - Dificuldade	
6 - Sons e Música
7 - Conclusão


Índice de Ilustrações:
Figura 1 - Interface Gráfica do jogo	
Figura 2 - Interface Gráfica pt2	
Figura 3 - Exemplo de powerup (hp)	
Figura 4 - Sprites e Imagens	




1 - Introdução:

Zombie Rampage é um jogo baseado em “Top Down Shooter” feito na framework MonoGame, ou seja, é um jogo 2D com a perspetiva vista de cima. 
Consiste em um jogo de sobrevivência, o objetivo do jogador é sobreviver a hordas de inimigos, para complicar a vida do jogador, a dificuldade do jogo aumenta a cada zombie que ele, para compensar, o jogador também vai ficando mais forte para ser capaz de lutar contra as hordas. 



2 - Movimentos e ações:

O jogador pode andar usando as teclas “W” (frente), “A” (esquerda), “D” (direita) e “S” (baixo).
O jogador pode disparar um tiro clicando no “Botão Esquerdo do Rato” ou segurar o “Botão Esquerdo do Rato” para disparar uma rajada de tiros.
O jogador pode pressionar a tecla “R” o jogador pode carregar a Arma.



3 - Interface

No canto superior esquerdo do jogo o jogador também tem acesso às suas estatitsticas que guardam os seguintes dados cruciais:

•	número de Kills da rodada atual (Kill Count)

•	highscore que regista o número de kills em ficheiro (High Score)

•	quantidade de munição disponível (Ammo)

•	quantidade de vida que possui (HP)

![image](https://github.com/DigitalGameDevTeam/Monogame---Second-Project/assets/148542897/010e769c-c0f5-453d-b6c6-23421ecb4b56)

Figura 1 - Interface Gráfica do jogo

Existe também um indicador que aparece enquanto o tempo de recarga da arma

![image](https://github.com/DigitalGameDevTeam/Monogame---Second-Project/assets/148542897/d9d9034e-cdaa-4489-80fa-9de437961d13)

Figura 2 - Interface Gráfica pt2

Mais um indicador que aparece quando recebe um powerup

![image](https://github.com/DigitalGameDevTeam/Monogame---Second-Project/assets/148542897/63bb3386-ea07-4d80-a8f7-6b254d074672)

Figura 3 - Exemplo de powerup (hp)



4 - Arte

Em termos de sprites, o jogo utiliza 3 sprites diferentes, 1 para o jogador e 2 para os inimigos.

O fundo do jogo é “tile generated”

![image](https://github.com/DigitalGameDevTeam/Monogame---Second-Project/assets/148542897/11864cbd-7243-4d2b-9d42-105c0da14b52)

Figura 4 - Sprites e Imagens



5 - Dificuldade

Sendo um jogo do estilo Arcade, foi adicionado um controle de dificuldade.

Á medida que o jogador for progredindo a dificuldade do jogo vai aumentando. 
Mais concretamente:

•	A cada 15 Kills os inimigos vão ficando mais fortes. 

•	E para acompanhar esse crescimento a cada 10 kills, um PowerUP.

Powerup realiza um aumento de um dos seguintes status (Vida, Velocidade de Movimento, Cooldown de Disparo, Munição Máxima, Tempo de Recarga).



6 - Sons e Música

Para efeitos sonoros, o jogo utiliza:

•	C418 Aria Math: música de fundo 
https://www.youtube.com/watch?v=atgjKEgSqSU )

•	Bullet Sound: é o efeito sonoro que irá tocar quando uma bala é disparada.

•	Reload Sound: é o efeito sonoro que irá tocar quando o jogador recarregar a arma.

•	Damage Sound: é o efeito sonoro que irá tocar quando o jogador tomar dano.


7 - Conclusão

Chegamos ao término deste projeto de desenvolvimento do nosso jogo topdown shooter para a disciplina de Técnicas de Desenvolvimento de Videojogos. Foi um percurso desafiador, mas igualmente enriquecedor. Fizemos um produto final de que nos podemos orgulhar.

Gostaríamos de destacar a importância do primeiro projeto de analisar um jogo existente, que foi essencial para compreendermos melhor os elementos-chave de um bom design e gameplay. Essa análise inicial permitiu-nos aplicar esses conhecimentos ao nosso próprio jogo de forma eficaz.

Este projeto não só reforçou as nossas competências técnicas, mas também demonstrou a importância do trabalho em equipa, crucial para o nosso desenvolvimento pessoal neste curso.


 













