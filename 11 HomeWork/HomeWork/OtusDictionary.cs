using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


/*
    Здравствуйте, Илья. Спасибо большое за вашу работу. Для начала давайте уточню, что пункт 1 и 3 действительно не могут работать одновременно, 
    однако, так как для набора минимально необходимого балла, чтобы сдать домашнюю работу, не обязательно делать 3 пункт, то вполне может
    существовать пункт 1 с использованием исключений. (принято)

    Теперь что касается вашей работы. Мне нравится, что вы инициализируете переменные в конструкторе и подробно комментируете код. 
    Правда, несколько смущает что комментарии начинаются с маленькой буквы, равно как и названия некоторых методов. 
    Напомню, что по общепринятой конвенции в C# методы всегда начинаются с большой буквы. (исправлено)

    Также, вижу, что вы реализовали разрешение коллизий с помощью открытой адресации. Это несомненно отличное решение, к сожалению, в задании предполагается,
    что вы реализуете закрытую адресацию. 
    То есть, если у элемента есть ключ, то по этому и только этому ключу будет находиться элемент. (исправлено)

    Плюс, проверьте, пожалуйста, работу индексаторов с ключом, превышающим размер массива, у меня такой вызов не сработал. (исправлено)

    Также, думаю, что при работе с индексатором в качестве value должна приниматься строка, а не ItemDictionary. 
    Ещё, в текущей реализации можно добавить несколько разных значений по одному ключу и они все будут храниться в _items. (исправлено)

    Предлагаю вам в качестве хранилища использовать массив строк, как это указано в задаче и реализовать задачу с ним. Возможно,
    придётся немного поломать голову над тем, как это сделать, однако, уверен, что будете приятно удивлены тем, как просто и элегантно можно сделать 
    это задание.(исправлено)

    Если же не получится и вы где-то застрянете, то пишите здесь и я с удовольствием вам помогу. А пока возвращаю работу вам.
 */


/*
    Реализуйте класс OtusDictionary, который позволяет оперировать int-овыми значениями в качестве ключей и строками в качестве значений. 
    Для добавления используйте метод void Add(int key, string value), а для получения элементов - string Get(int key). 
    Внутреннее хранилище реализуйте через массив. 
    При нахождении коллизий, создавайте новый массив в два раза больше и не забывайте пересчитать хеши для всех уже вставленных элементов. 
    Метод GetHashCode использовать не нужно и массив/список объектов по одному адресу создавать тоже не нужно (только один объект по одному индексу). 
    Словарь не должен давать сохранить null в качестве строки, соответственно, проверять заполнен элемент или нет можно сравнивая строку с null.
 */
namespace HomeWork
{
    /// <summary>
    /// Кастомный словарь
    /// </summary>
    internal class OtusDictionary
    {
        /// <summary>
        /// Первоначальная длина коллекции OtusDictionary
        /// </summary>
        private int _size;
        /// <summary>
        /// Текущая позиция элемента
        /// </summary>
        private int _nowPosition;
        /// <summary>
        /// Ключи словаря
        /// </summary>
        private int[] _keys;
        /// <summary>
        /// значения словаря
        /// </summary>
        private string[] _values;

        /// <summary>
        /// конструктор(по умолчанию размер OtusDictionary равен 32 
        /// </summary>
        public OtusDictionary()
        {
            _size = 32;
            _nowPosition = 0;
            _keys = new int[_size];
            _values = new string[_size];
        }

        /// <summary>
        /// Получить key при помощи хеш таблицы
        /// </summary>
        /// <param name="key"> Ключ </param>
        /// <returns>Хеш - ключ</returns>
        private int HashKey(int key) => key % _size;

        #region 1 Реализуйте метод Add с неизменяемым массивом размером 32 элемента(исключение, если уже занято место).

        /// <summary>
        /// Добавить элемент
        /// </summary>
        /// <param name="key">Ключ-значение (int)</param>
        /// <param name="value">Значение (string)</param>
        //TODO: 1 задание противоречит 3 "(исключение, если уже занято место)"
        //"Реализуйте увеличение массива в два раза при нахождении коллизий"
        public void Add(int key, string value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Значение не может быть null");
            }
            //if (_nowPosition >= _size)
            //{ 
            //    throw new ArgumentNullException("место закончилось!");
            //}
            // закомментированный фрагмент для первого задания  "(исключение, если уже занято место)"
            int keyHash = HashKey(key);
            if (_nowPosition >= _size)
            {
                LengthIncrease();
                keyHash = HashKey(key);
            }
            if (_values[keyHash] is not null)
            {
                if (_keys[keyHash] == key)
                {
                    throw new Exception("такой ключ уже добавлен!");
                }
                else
                {
                    bool flagCollision = LengthIncrease();
                    keyHash = HashKey(key);
                    if (_values[keyHash] is not null)
                    {
                        flagCollision = true;
                    }
                    while (flagCollision)
                    {
                        // Число 1 и 65 при размерности массива 32 и 64 имеют двойное попадание в коллизию, поэтому результирующий словарь должен быть 128
                        flagCollision = LengthIncrease();
                        keyHash = HashKey(key);
                        if (_values[keyHash] is not null)
                        {
                            flagCollision = true;
                        }
                    }
                    _keys[keyHash] = key;
                    _values[keyHash] = value;
                    _nowPosition++;
                }
            }
            else
            {
                _keys[keyHash] = key;
                _values[keyHash] = value;
                _nowPosition++;
            }
        }
        #endregion

        #region 2 Реализуйте метод Get.

        /// <summary>
        /// Получить элемент по ключу
        /// </summary>
        /// <param name="key">Значение ключа по которому нужно вернуть значение</param>
        /// <returns>Значение элемента</returns>
        public string Get(int key)
        {
            int _hashKey = HashKey(key);
            if (_values[_hashKey] is not null)
            {
                return _values[_hashKey];
            }
            else
            {
                throw new NullReferenceException();
            }
        }
        #endregion

        #region 3 Реализуйте увеличение массива в два раза при нахождении коллизий
        /// <summary>
        /// Увеличение длины основного массива OtusDictionary
        /// </summary>
        private bool LengthIncrease()
        {
            _size *= 2;
            var _keysNew = new int[_size];
            var _valuesNew = new string[_size];
            int _index = 0;
            for (int i = 0; i < _keys.Length - 1; i++)
            {
                if (_values[i] is not null)
                {
                    _index = HashKey(_keys[i]);
                    if (_valuesNew[_index] is not null)
                    {
                        return true;
                    }
                    _keysNew[_index] = _keys[i];
                    _valuesNew[_index] = _values[i];
                }
            }
            _keys = _keysNew;
            _values = _valuesNew;
            _keysNew = null;
            _valuesNew = null;
            return false;
        }
        #endregion

        #region 5 Добавьте к классу возможность работы с индексатором
        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <returns>Значение ItemDictionary</returns>
        public string? this[int index]
        {
            get
            {
                if (_size <= index)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                {
                    return _values[index];
                }
            }
            set => _values[index] = value;
        }
        #endregion
    }
}


