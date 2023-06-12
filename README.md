# DINONUGGY
Este projeto é sobre um jogo 2D desenvolvido em Monogame e em C# cujo objetivo é sobreviver o máximo de tempo possível, obtendo assim a maior pontuação, enquanto o jogador se desvia de inimigos.

---

**Grupo:**

Gonçalo Silva- a25970;

Gustavo Gonçalves- a25960;

Miguel Sousa- a25977;

---

## Estrutura do Código

Uma vez que o jogo foi desenvolvido em C#, o código está dividido em classes:

1. ScoreManager

2. Objetos

3. Bullet

4. Donut 

5. Ground

6. Homer 

7. Marge

8. Player

9. Câmara

10. Game1

11. Sounds

---

## Análise das diversas classes

### ScoreManager:

Nesta classe estão incluídos métodos para iniciar o jogo e para atualizar o Score do jogador com base no tempo de jogo e status do jogador. Esta é uma classe estática.

```cs
 public static class ScoreManager
    {
        public static int Score { get; private set; }
        private static double ScoreTimer = 0;
        private static DateTime StartTime;


        public static void StartGame()
        {
            Score = 0;
            StartTime = DateTime.Now;
            ScoreTimer = 0;
        }

        public static void UpdateScore(GameTime gameTime,Player player)
            {
            if (player.isDead)
            {
                return;
            }

            double elapsedSeconds = (DateTime.Now - StartTime).TotalSeconds;
            ScoreTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (ScoreTimer >= 1)
            {
                Score = (int)(elapsedSeconds * 7);
                ScoreTimer = 0;
            }
        }
    }
}
```

A propriedade `Score` é um inteiro de acesso público que representa a pontuação atual do jogador.

`ScoreTimer` é uma variável privada estática do tipo double, inicializada com 0. Ela é usada para acompanhar o tempo passado desde a última atualização da pontuação.

`StartTime` é uma variável privada estática do tipo `DateTime` que armazena o horário de início do jogo.

O método `StartGame` é responsável por inicializar o estado do jogo. Ele define a pontuação como 0, define `StartTime` como o tempo atual e redefine `ScoreTimer` como 0.

O método `UpdateScore` recebe dois parâmetros: `gameTime` do tipo `GameTime` e `player` do tipo `Player`.

Primeiro, ele verifica se o jogador está morto. Se sim, retorna imediatamente sem atualizar a pontuação.

O tempo decorrido em segundos é calculado subtraindo `StartTime` do tempo atual e obtendo a propriedade `TotalSeconds`.

O `ScoreTimer` aumentado conforme o tempo de jogo decorrido em milissegundos.

Se o `ScoreTimer` exceder ou for igual a 1 ms, a pontuação é atualizada com base no tempo decorrido multiplicado por 7. Esse fator de multiplicação parece arbitrário e pode precisar de ajustes dependendo dos requisitos do seu jogo.

Em seguida, o `ScoreTimer` é redefinido como 0.

***

### Objetos:

Classe base das quais outras vão herdar parâmetros e funções.

```cs
 public class Objetos
    {
        public Texture2D texture; 
        public Vector2 velocity;
        public Vector2 position;
        public int height = 70, width = 70;
        public bool active;


        public virtual Rectangle HitBox
        {
            get => new Rectangle((int)position.X, (int)position.Y, height, width);
        }
        public Objetos(Texture2D texture, Vector2 position, bool active)
        {
            this.texture = texture;
            this.position = position;
            this.active = active;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, HitBox,Color.Red);

            
        }


        protected bool CheckCollision( Objetos obj2)
        {
            return this.HitBox.Intersects(obj2.HitBox);
        }


    }
```

`texture`: variável do tipo `Texture2D` para representar a textura do objeto.

`velocity`: variável do tipo `Vector2` para representar a velocidade do objeto.

`position`: variável do tipo `Vector2` para representar a posição do objeto.

`height`: variável do tipo `int` para representar a altura do objeto.

 `width`: variável do tipo `int` para representar a largura do objeto.

`active`: variável do tipo `bool` para indicar se o objeto está ativo.

A propriedade `HitBox` retorna um retângulo com base na posição, altura e largura do objeto.

O construtor da classe recebe a textura, posição e um valor booleano para indicar se o objeto está ativo. Ele inicializa os campos correspondentes com os valores fornecidos.

O método `Draw` recebe um objeto `SpriteBatch` como parâmetro e desenha a textura do objeto na área delimitada pelo `HitBox` usando a cor `Color.Red`.

O método `CheckCollision` verifica se há colisão entre o objeto atual e outro objeto (`obj2`). Ele utiliza o método `Intersects` do retângulo `HitBox` para verificar e retorna o resultado como um valor booleano.

*** 

### Bullet:

A classe `Donuts` herda da classe `Objetos`.

```cs
public class Bullet:Objetos
    {
        int speed;
        public bool isActive { get; set; }

        public override Rectangle HitBox
        {
            get => new Rectangle((int)position.X, (int)position.Y, height , width);
        }


        public Bullet( Texture2D texture,Vector2 position,int width,int heigh):base(texture,position,true)
        {
            this.position = position;
            this.velocity = Vector2.Zero;
            this.texture = texture;
            this.speed = 9;
            this.width = width;
            this.height = heigh;
            this.isActive = true;

        }

        
        public void Update(GameTime gameTime, List<Marge> margeList,List<Homer> homersList)
        {
            velocity.X = speed;
            position += velocity;


            foreach (Marge marge in margeList)
            {
                if (HitBox.Intersects(marge.HitBox))
                {
                    Sounds.margeHit.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                    margeList.Remove(marge);
                    isActive = false;
                    break;
                }
            }
            foreach(Homer homer in homersList)
            {
                if (HitBox.Intersects(homer.HitBox))
                {
                    Sounds.homerHit.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                    isActive = false;
                    break;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, HitBox, Color.White);
        }

        

       
    }
```

`speed`: variável do tipo `int` para armazenar a velocidade do objeto `Bullet`.

`isActive`: variável do tipo `bool` que determina se o objeto `Bullet` está ativo ou não.

A classe Bullet substitui a propriedade `Hitbox` da classe Objetos. Ela retorna um retângulo que representa a área de colisão da bala com base na sua posição, largura e altura.

A classe Bullet possui um construtor que recebe um objeto `Texture2D` como textura da bala, um objeto `Vector2` para a posição inicial, um `int` para a largura e para a altura. Ele inicializa várias propriedades, como a posição, textura, speed, largura, altura e isActive.

O método `Update` é responsável por atualizar a posição da bala com base na sua velocidade e verificar colisões com objetos nas listas `margeList` e `homersList`. Se ocorrer uma colisão com um objeto `Marge`, ele reproduz um som, remove o objeto `Marge` da lista e define `isActive` como false. Se ocorrer uma colisão com um objeto `Homer`, ele reproduz um som diferente e define `isActive` como false.

O método `Draw` é responsável por desenhar a bala na tela usando um `SpriteBatch`. Ele usa o `HitBox` da bala para determinar a posição e o tamanho da textura desenhada. 



### Donut:

A classe `Donuts` herda da classe `Objetos`. Esta classe é responsável por inicializar e colocar os Donuts no ecrã.

```cs
public class Donuts : Objetos
    {


        public Donuts(Texture2D texture, Vector2 position):base(texture,position, true)
        {
            this.texture = texture;
            this.position = position;
            height = 80;
            width = 80;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, HitBox, Color.LightGoldenrodYellow);
        }

    }
}
```

O construtor da classe recebe uma textura e uma posição como parâmetros e chama o construtor da classe base `Objetos` com esses parâmetros. Ele também define a altura e a largura do objeto como 80.

O método `Draw` é um override do método da classe base. Ele recebe um objeto `SpriteBatch` como parâmetro e desenha a textura do objeto `Donuts` na área limitada pela `HitBox` usando a cor `Color.LightGoldenrodYellow`.

***

### Ground:

A classe `Ground` herda da classe `Objetos`. Esta classe inicializa e desenha o chão. 

```cs
 public class Ground : Objetos
    {
        public override Rectangle HitBox
        {
            get => new Rectangle((int)position.X, (int)position.Y, width, height);
        }
        public Ground(Texture2D texture, Vector2 position, int height, int width) : base(texture, position, true)
        {
            this.texture = texture;
            this.position = position;
            this.width = width;
            this.height = height;
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, HitBox, Color.White);
        }
    }
```

Esta classe tem uma propriedade `HitBox` que é um override da propriedade da classe base `Objetos`. Essa propriedade retorna um novo retângulo definido pela posição `position`, largura `width` e altura `height`.

O método `Draw` é um override do método da classe base. Ele recebe um objeto `SpriteBatch` como parâmetro e desenha a textura do objeto `Ground` na área limitada pela `HitBox` usando a cor `Color.White`.

***

### Homer:

A classe `Homer` herda da classe `Objetos`. Esta classe inicializa um dos inimigos do jogo, o Homer.

```cs
 public class Homer : Objetos
    {
        public float speed;
        private float spawnTimer;
        private float spawnInterval = 2.5f;
        public int damage;
        public bool isCollided = false;
        public override Rectangle HitBox => new Rectangle((int)position.X, (int)position.Y, width, height-10);

        public Homer(Texture2D texture, Vector2 position, bool active, int damage) : base(texture, position, active)
        {
            this.texture = texture;
            this.position = position;
            this.active = active;
            height = 90;
            width = 80;
            speed = 225;
            this.damage = damage;
            spawnTimer = spawnInterval;
        }

        public void Update(double deltaTime,Player player)
        {
            if (player.isDead)
            {
                return; 
            }

            velocity.X = -speed;
            position += velocity * (float)deltaTime;

            spawnTimer -= (float)deltaTime;

            if (spawnTimer <= 0 )
            {
                SpawnHomer();
                spawnTimer = spawnInterval;
            }
        }

        private void SpawnHomer()
        {
            if (Game1.listaHomers.Any(homer => homer.position == new Vector2(Game1.screenWidth * 2, Game1.screenHeight - 180)))
            {
                return; 
            }
            Homer newHomer = new Homer(texture, new Vector2(Game1.screenWidth * 2, Game1.screenHeight - 180), true, damage);
            Game1.listaHomers.Add(newHomer);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, HitBox, Color.White);
         
        }
       
    }        
```

`speed`: variável do tipo `float` para representar a velocidade do objeto `Homer`.

`spawnTimer`: variável do tipo `float` para controlar o intervalo de tempo entre os spawns do objeto `Homer`.

`spawnInterval`: variável do tipo `float` para definir o intervalo de tempo entre os spawns do objeto `Homer`.

`damage`: variável do tipo `int` para representar o dano do objeto `Homer`.

`isCollided`: variável do tipo `bool` para indicar se o objeto `Homer` colidiu com algo.

`HitBox`: propriedade que retorna um retângulo com base na posição, largura e altura do objeto `Homer`.

O construtor da classe recebe uma textura, posição, valor booleano para indicar se o objeto está ativo e um valor para o dano como parâmetros. Ele chama o construtor da classe base `Objetos` com esses parâmetros e define os valores correspondentes nos campos da classe. Também define a altura e a largura do objeto como 80, a velocidade como 225 e inicializa o `spawnTimer` com o valor do `spawnInterval`.

O método `Update` recebe um valor `deltaTime` do tipo `double` e um objeto `Player` como parâmetros. Ele atualiza a posição do objeto `Homer` com base na velocidade e no tempo decorrido. Também reduz o `spawnTimer` com base no `deltaTime` e, quando o `spawnTimer` chega a zero, chama o método `SpawnHomer` para criar um novo objeto `Homer`.

O método `SpawnHomer` verifica se já existe um objeto `Homer` na mesma posição antes de criar um novo com base na textura, posição, valor booleano para ativar e dano. A seguir, adiciona o novo objeto `Homer` à lista `listaHomers` da classe `Game1`.

O método `Draw` é um override do método da classe base. Ele recebe um objeto `SpriteBatch` como parâmetro e desenha a textura do objeto `Homer` na área limitada pela `HitBox` usando a cor `Color.White`.

****

### Marge:

A classe `Marge` herda da classe `Objetos`. Esta classe inicializa um dos inimigos do jogo, a Marge.

```cs
 public class Marge : Objetos
    {
        public float speed;
        private float spawnTimer;
        private float spawnInterval = 3.0f;
        public int damage;
        public bool isCollided = false;

        public override Rectangle HitBox => new Rectangle((int)position.X, (int)position.Y, width, height);

        public Marge(Texture2D texture, Vector2 position, bool active, in damage) : base(texture, position, active)
        {
            this.texture = texture;
            this.position = position;
            this.active = active;
            height = 120;
            width = 50;
            speed = 180;
            this.damage = damage;
            spawnTimer = spawnInterval;
        }

        public void Update(double deltaTime, Player player)
        {
            if (player.isDead)
            {
                return;
            }
            
            velocity.X = -speed;
            position += velocity * (float)deltaTime;

            spawnTimer -= (float)deltaTime;

            if (spawnTimer <= 0)
            {
                SpawnMarge();
                spawnTimer = spawnInterval;
            }
        }

        private void SpawnMarge()
        {
            if (Game1.listaMarge.Any(marge => marge.position == new Vector2(Game1.screenWidth * 2, Game1.screenHeight - 220)))
            {
                return;
            }
            Marge newMarge = new Marge(texture, new Vector2(Game1.screenWidth * 2, Game1.screenHeight - 220), true, damage);
            Game1.listaMarge.Add(newMarge);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, HitBox, Color.White);
        }
```

`speed`: variável do tipo `float` para representar a velocidade do objeto `Marge`.

`spawnTimer`: variável do tipo `float` para controlar o tempo de intervalo entre os spawns do objeto `Marge`.

`spawnInterval`: variável do tipo `float` para definir o intervalo de tempo entre os spawns do objeto `Marge`.

`damage`: variável do tipo `int` para representar o dano do objeto `Marge`.

`isCollided`: variável do tipo `bool` para indicar se o objeto `Marge` colidiu com algo.

`HitBox`: uma propriedade que retorna um retângulo com base na posição, largura e altura do objeto `Marge`.

O construtor da classe recebe uma textura, uma posição, um valor booleano para indicar se o objeto está ativo e um valor para o dano como parâmetros. Ele chama o construtor da classe base `Objetos` com esses parâmetros e define os valores correspondentes nos campos da classe. Também define a altura e a largura do objeto como 100, a velocidade como 150 e inicializa o `spawnTimer` com o valor do `spawnInterval`.

O método `Update` recebe um valor `deltaTime` do tipo `double` e um objeto `Player` como parâmetros. Ele atualiza a posição do objeto `Marge` com base na velocidade e no tempo decorrido. Também reduz o `spawnTimer` com base no `deltaTime` e, quando o `spawnTimer` chega a zero, chama o método `SpawnM`

***

### Player:

A classe `Player` herda da classe `Objetos` e possui campos adicionais para velocidade (`speed`), gravidade (`gravity`), status de vida (`isDead`), status de estar no ar (`isMidair`), status de colisão (`isCollided`) e pontos de vida (`hp`).

```cs
public class Player : Objetos
    {
        float speed, gravity;
        public bool  isDead, isMidair, isCollided;
        public int hp;
        public override Rectangle HitBox
        {
            get => new Rectangle((int)position.X, (int)position.Y, height+10, width);
        }

        //Contructor
        public Player(Texture2D texture, Vector2 position ) : base(texture, position,true)
        {
            speed = 150;
            gravity = 190;
            isDead  = false;
            isCollided = false;
            hp = 100;
        }


        public void Update(double deltaTime, List<Objetos> gameObjects,List<Homer>homers,List<Marge>marges)
        {
            isMidair = true;
            KeyboardState kState = Keyboard.GetState();
            bool isKeyPressed=false;
            Gravity(deltaTime);
            HandleCollision(gameObjects,homers,marges);

            if (kState.IsKeyDown(Keys.A) || kState.IsKeyDown(Keys.Left)) 
            {
                velocity.X = -speed;
                isKeyPressed = true;
            }
            if (kState.IsKeyDown(Keys.D) || kState.IsKeyDown(Keys.Right))
            {
                velocity.X = speed;

                isKeyPressed = true;
            }
            if (kState.IsKeyDown(Keys.Space))
            {
                Jump();

                isKeyPressed = true;

            }
            if(!isKeyPressed)
            {
                velocity.X = 0;
            }
            if (hp <=0 &&  !isDead)
            {
                Die();
                
            }
           
            position += velocity* (float)deltaTime;
        }



        public override void Draw(SpriteBatch spriteBatch)
        {
           
            spriteBatch.Draw(texture, HitBox, Color.White);
        }



        public void HandleCollision(List<Objetos> gameObjects, List<Homer> homers,List<Marge> marges)
        {
            if (isDead == true) return;
            foreach (Objetos gameObject in gameObjects)
            {
                if (CheckCollision(gameObject))
                {
                    if (gameObject is Ground)
                    {
                        if (velocity.Y > 0)
                        {

                            velocity.Y = 0;
                            isMidair = false;
                        }
                        else if (velocity.Y < 0)
                        {

                            velocity.Y = 0;
                        }
                        else if (velocity.X > 0)
                        {
                            float overlap = this.HitBox.Right - gameObject.HitBox.Left;
                            position.X -= overlap;
                            velocity.X = 0;
                        }
                        else if (velocity.X < 0)
                        {
                            float overlap = gameObject.HitBox.Right - this.HitBox.Left;
                            position.X += overlap;
                            velocity.X = 0;
                        }
                    }


                }

            }
            List<Homer> collidedHomers = new List<Homer>();
            List<Marge> collidedMarge = new List<Marge>();

            foreach (Homer homer in homers)
            {
                if (CheckCollision(homer) && !homer.isCollided)
                {
                    hp -= 25;
                    Sounds.damage.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                    collidedHomers.Add(homer);
                    homer.isCollided = true;
                    homer.active = false;
                }
            }
            foreach (Marge marge in marges)
            {
                if (CheckCollision(marge) && !marge.isCollided)
                {
                    hp -= 50;
                    Sounds.damage.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                    collidedMarge.Add(marge);
                    marge.isCollided = true;
                    marge.active = false;
                }
            }
            

            foreach (Marge marge in collidedMarge)
            {
                marges.Remove(marge);
            }
            foreach (Homer collidedHomer in collidedHomers)
            {
                homers.Remove(collidedHomer);
            }
            if (marges.All(marge => marge.isCollided))
            {
                isCollided = false;
            }
            if (homers.All(homer => homer.isCollided))
            {
                isCollided = false;
            }
            

        }
        public void Die()
        {
        
                speed = 0;
                isDead = true;
                Sounds.death.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);
                Jump();
                
        }

        public void Gravity(double deltaTime) {
            if (isMidair) {
                velocity.Y += (float)(gravity * deltaTime);
            }
        }


        private void Jump()
        {
            if (!isMidair)
            {
                velocity.Y -= 200;
                isMidair = true;
            }

        }
        
        private void JumpNoSound()
        {
            if (!isMidair)
            {                
                velocity.Y -= 200;
                isMidair = true;
            }

        }
        public void UpdateInput(List<Bullet> bullets, GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if(!isDead) 
            {
                if (keyboardState.IsKeyDown(Keys.N) && canShoot)
                {
                    Shoot(bullets);
                    canShoot = false;
                    shootTimer = TimeSpan.Zero;
                }

                if (!canShoot)
                {
                    shootTimer += gameTime.ElapsedGameTime;

                    if (shootTimer >= shootInterval)
                    {
                        canShoot = true;
                    }
                }
            }
            
        }

        private void Shoot(List<Bullet> bullets)
        {
            Vector2 bulletPosition = new Vector2(position.X + HitBox.Width, position.Y + HitBox.Height / 2);
            Texture2D bulletTexture = content.Load<Texture2D>("BULLET");

            Bullet bullet = new Bullet(bulletTexture, bulletPosition, 5, 10);
            bullets.Add(bullet);

            canShoot = false;
        }
  
```

O construtor da classe recebe uma textura e uma posição como parâmetros. Ele inicializa os campos correspondentes com os valores fornecidos e define os valores iniciais para as outras variáveis.

O método `Update` é responsável por atualizar a lógica do jogador em cada frame. Ele recebe o tempo decorrido em segundos desde o último frame (`deltaTime`), uma lista de objetos do jogo, uma lista de `Homer` e uma lista de `Marge`. Ele lida com entrada do teclado, aplica a gravidade, lida com colisões, verifica se o jogador morreu e atualiza a posição do jogador.

O método `Draw` é responsável por desenhar o jogador na tela. Ele usa a textura e o `HitBox` para realizar o desenho.

O método `HandleCollision` verifica colisões com os inimigos do jogo, `Homer` e `Marge`. Se houver colisões, ele lida com elas de acordo com a lógica do jogo, atualiza os status do jogador e remove os objetos colididos das listas, se necessário.

O método `Die` é chamado quando o jogador morre. Ele ajusta a velocidade, define o status `isDead` como verdadeiro, reproduz um som de morte e realiza um salto.

O método `Gravity` aplica a força da gravidade ao jogador se este estiver no ar.

O método `Jump` é chamado para fazer o jogador saltar se não estiver já no ar. Ele ajusta a velocidade vertical para cima e define o status `isMidair` como verdadeiro.

***

### Câmara:

A classe `Camera` é responsável por controlar a transformação da câmera no jogo. Ela possui uma propriedade `Transform` que retorna a matriz de transformação atual.

```cs
internal class Camera
    {
        public Matrix Transform { get; private set; }
        public void Follow(Player target)
        {
            var position = Matrix.CreateTranslation(-target.position.X - (target.HitBox.Width / 2),-360  , 0);
            var offset = Matrix.CreateTranslation(Game1.screenWidth / 2, Game1.screenHeight / 2, 0);
            Transform = position * offset;
        }
    }
```

O método `Follow` recebe um objeto `Player` como alvo e ajusta a câmara com base na posição do jogador. A matriz `position` é criada para ajustar a posição da câmara para que ela siga o jogador horizontalmente, mas permaneça em uma posição fixa verticalmente. A matriz `offset` é criada para ajustar a posição da câmara para que o jogador fique no centro na tela. A transformação final é calculada multiplicando as matrizes `position` e `offset` e é atribuída à propriedade `Transform`.

****

### Game1:

A classe `Game1` é a classe principal do jogo e herda da classe `Game` do Monogame. Nesta classe, estão os métodos de inicialização, load, update e draw do jogo.

```cs
public class Game1 : Game
    {
        public static int screenWidth = 1280;
        public static int screenHeight = 720;
        GraphicsDeviceManager _graphics;
        SpriteBatch spriteBatchPlayer, spriteBatchUI, spriteBatchBC;
        Player player;
        Ground ground;
        Homer homer;
        Donuts donut;
        Marge marge;
        Bullet bullet;
        public static List<Objetos> objetos = new List<Objetos>();
        public static List<Homer> listaHomers = new List<Homer>();
        public static List<Marge>listaMarge = new List<Marge>();
        Texture2D background;

        public static List<Bullet> bullets = new List<Bullet>();
        Camera cam;
        private TimeSpan gameTimeElapsed;
        private SpriteFont _font;
        bool isTimePaused = false;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            this.IsMouseVisible = true;
            _graphics.PreferredBackBufferHeight = screenHeight; 
            _graphics.PreferredBackBufferWidth = screenWidth; 
            _graphics.ApplyChanges();
            

            ScoreManager.ScoreManager.StartGame();
            cam = new Camera();
            base.Initialize();
        }

        protected override void LoadContent()
        {
          
            spriteBatchPlayer = new SpriteBatch(GraphicsDevice);
            spriteBatchUI= new SpriteBatch(GraphicsDevice);
            spriteBatchBC = new SpriteBatch(GraphicsDevice);
            ground = new Ground(Content.Load<Texture2D>("Ground"), new Vector2(0, screenHeight-100),100, screenWidth*2);
            player = new Player(Content.Load<Texture2D>("NUGGY"), new Vector2(screenWidth/ 4 - 35, screenHeight / 2 - 35),Content);
            homer = new Homer(Content.Load<Texture2D>("HOMER"), new Vector2(screenWidth * 2 , screenHeight -180), true, 25);
            marge = new Marge(Content.Load<Texture2D>("MARGE"), new Vector2(screenWidth * 2, screenHeight - 220), true, 50);
            donut = new Donuts(Content.Load<Texture2D>("DONUT"), new Vector2(screenWidth -300, 0));
            bullet = new Bullet(Content.Load<Texture2D>("BULLET"), Vector2.Zero,5,10);
            background = Content.Load<Texture2D>("Background");
            _font = Content.Load<SpriteFont>("Fonte");
            Sounds.LoadSounds(Content);
            listaHomers.Add(homer);
            objetos.Add(ground);
            listaMarge.Add(marge);
            
        }



        protected override void Update(GameTime gameTime)
        {
            if (player.isDead)
            {
                
                isTimePaused = true;
            }
            if (!isTimePaused)
            {
                gameTimeElapsed += gameTime.ElapsedGameTime;
            }
            double deltaTime = gameTime.ElapsedGameTime.TotalSeconds;

           
            for (int i = listaHomers.Count - 1; i >= 0; i--)
            {
                Homer homer = listaHomers[i];
                homer.Update(deltaTime, player);


                if (homer.position.X < -homer.width)
                {
                    listaHomers.RemoveAt(i);
                }
            }
            for (int i = listaMarge.Count - 1; i >= 0; i--)
            {
                Marge marge = listaMarge[i];
                marge.Update(deltaTime, player);


                if (marge.position.X < -marge.width)
                {
                    listaMarge.RemoveAt(i);
                }
            }
           
            player.UpdateInput(bullets,gameTime);
            player.Update(deltaTime, objetos, listaHomers,listaMarge);
            if (!player.isDead)
            {
                if (player.position.Y > screenHeight)
                {
                    player.hp = 0;
                    player.Die();
                }
            }
            
                bullet.position = player.position;
            foreach (Bullet bullet in bullets)
            {
                bullet.Update(gameTime, listaMarge,listaHomers); 
            }

            bullets.RemoveAll(bullet => !bullet.isActive);
            ScoreManager.ScoreManager.UpdateScore(gameTime,player);
            cam.Follow(player);
            base.Update(gameTime);
        }



        protected override void Draw(GameTime gameTime)
        {
            spriteBatchBC.Begin();
            spriteBatchBC.Draw(background,new Rectangle(0,0,screenWidth,screenHeight), Color.White);
            spriteBatchPlayer.Begin(transformMatrix: cam.Transform);
            spriteBatchUI.Begin();
            spriteBatchUI.DrawString(_font, "HP:" + player.hp , new Vector2(10, 50), Color.White);
            spriteBatchUI.DrawString(_font, ScoreManager.ScoreManager.Score.ToString(), new Vector2(screenWidth-220,20), Color.White);
            spriteBatchUI.DrawString(_font, "Tempo de Jogo:" + gameTimeElapsed.TotalSeconds.ToString("0.00")  +  " segundos", new Vector2(10,10), Color.White);
            ground.Draw(spriteBatchPlayer);
            foreach(Homer item in listaHomers)
            {
                if (item.active == true)
                {
                    item.Draw(spriteBatchPlayer);
                }
            }
            foreach (Marge item in listaMarge)
            {
                if (item.active == true)
                {
                    item.Draw(spriteBatchPlayer);
                }
            }
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatchPlayer); 
            }
            player.Draw(spriteBatchPlayer);
            donut.Draw(spriteBatchUI);
            
           
            spriteBatchBC.End();
            spriteBatchPlayer.End();
            spriteBatchUI.End();
            base.Draw(gameTime);
        }

        
    }

            spriteBatchPlayer.End();
            spriteBatchUI.End();
            base.Draw(gameTime);
        }

        
    }
```

No método `Initialize`, as configurações iniciais do jogo são definidas, como a resolução da tela e a geração da instância da classe `Camera`. O método `LoadContent` é responsável por carregar os recursos do jogo, como as texturas e os sons, e pela criação das instâncias dos objetos do jogo, como o jogador, o chão, os inimigos, etc.

O método `Update` é chamado a cada frame e é onde a lógica do jogo é atualizada. Aqui encontra-se o código para atualizar a posição dos inimigos, o jogador, calcular a pontuação, verificar se o jogador está morto, etc.

O método `Draw` é responsável por desenhar os elementos do jogo na tela. Aqui encontra-se o código para desenhar o jogador, os inimigos, o chão, a pontuação e outros elementos visuais do jogo.

Estas são as partes principais do código do jogo, onde a lógica e a renderização ocorrem.

****

### Sounds:

A classe `Sounds` é uma classe estática que contém as referências para os efeitos sonoros do jogo. Ela possui três variáveis estáticas do tipo `SoundEffect`: `death`, `damage` e `jump`.

```cs
public static class Sounds
    {
        public static SoundEffect death,damage;
        public static SoundEffect jump;

        public static void LoadSounds(ContentManager Content)
        {
            death = Content.Load<SoundEffect>("death");
            damage = Content.Load<SoundEffect>("damage");
            jump = Content.Load<SoundEffect>("Jump");
            homerHit= Content.Load<SoundEffect>("Boring");
            margeHit= Content.Load<SoundEffect>("OOF");
        }
    }
}
```

O método `LoadSounds` é responsável por carregar os ficheiros de som correspondentes aos efeitos sonoros. Ele recebe um parâmetro `ContentManager`, que é responsável por gerenciar o conteúdo do jogo. Os arquivos de som são carregados usando o método `Load` do `ContentManager`, passando o nome do arquivo como parâmetro.

Esta classe pode ser utilizada para reproduzir os efeitos sonoros em diferentes partes do código do jogo, por exemplo, quando o jogador morre, sofre/causa dano ou salta.

***

