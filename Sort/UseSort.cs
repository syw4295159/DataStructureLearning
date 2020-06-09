using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseSort : MonoBehaviour
{
    int[] array = new int[10];
    void Start()
    {
        array[0] = 90;
        array[1] = 10;
        array[2] = 50;
        array[3] = 80;
        array[4] = 30;
        array[5] = 70;
        array[6] = 40;
        array[7] = 60;
        array[8] = 20;
        array[9] = 0;

        //SimpleBubbleSort();
        //NormalBubbleSort_Version0();
        //UpgradeBubbleSort();
        //CocktailSort();

        //SelectSort();

        //InsertSort();

        //ShellSort();

        //HeadSort();

        //MergeSort_recursion();
        //MergeSort_normal();

        //FastSort(); 
        //Test();


        DebugArray();
    }
    public void Test()
    {
    }

    #region 冒泡排序 时间复杂度O(n^2)
    private void Swap(int i,int j)
    {
        int temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    //冒泡简单版
    private void SimpleBubbleSort()
    {
        int index = 0;
        int exchangeindex = 0;
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                index++;
                if(array[i] > array[j])
                {
                    exchangeindex++;
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }
        Debug.LogFormat("总共循环了{0} 交换了{1}", index, exchangeindex);//45 31
    }
    //冒泡正常版版本0
    private void NormalBubbleSort_Version0()
    {
        int index = 0;
        int exchangeindex = 0;
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = array.Length - 1; j >= i; j--)
            {
                index++;
                //Debug.LogFormat("{0} 与 {1} 比较", i, j);
                if (array[i] > array[j])
                {
                    exchangeindex++;
                    //Debug.LogFormat("因为{0} > {1},所以调换", array[i], array[j]);
                    var temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }
        }
        Debug.LogFormat("总共循环了{0} 交换了{1}", index, exchangeindex);//55 7
    }
    //冒泡升级版:避免多余的比较
    private void UpgradeBubbleSort()
    {
        int index = 0;
        int exchangeindex = 0;
        bool flag = true;
        for (int i = 0; i < array.Length && flag; i++)
        {
            flag = false;
            //i的每一次增长，都会让一个最大值冒泡到数组的最后一个成员，所以遍历就会减少一个成员
            for (int j = 0; j < array.Length - 1 - i; j++)
            {
                index++;
                if (array[j] > array[j+ 1])
                {
                    exchangeindex++;
                    Swap(j, j + 1);
                    flag = true;
                }
            }
        }
        Debug.LogFormat("总共循环了{0} 交换了{1}", index, exchangeindex);//45 31
    }
    //冒泡升级版:双冒泡排序
    private void CocktailSort()
    {
        int index = 0;
        int exchangeindex = 0;
        int left = 0;  //                初始化数组a的最左边索引
        int right = array.Length - 1;//           初始化数组a的最右边索引
        while (left < right)
        { //左右索引比较
            for (int i = left; i < right; i++)//前半轮，将最大的移到最右边
            {
                if (array[i] > array[i + 1])
                {
                    Swap(i, i + 1);
                    exchangeindex++;
                }
                index++;
            }
            right--;  //  比较范围左移一位
            for (int i = right; i > left; i--)//后半轮，将最小的移到最左边
            {
                if (array[i - 1] > array[i])
                {
                    Swap(i - 1, i);
                    exchangeindex++;
                }
                index++;
            }
            left++;//  比较范围右移一位
        }
        Debug.LogFormat("总共循环了{0} 交换了{1}", index, exchangeindex);//45 31
    }
    #endregion

    #region 选择排序 时间复杂度O(n^2) 效率略高于冒泡排序
    private void SelectSort()
    {
        int min = 0;
        for (int i = 0; i < array.Length; i++)
        {
            min = i;
            for (int j = i; j < array.Length; j++)
            {
                //这里是通过循环更新来选择最小值下标
                if (array[j] < array[min])
                {
                    min = j;
                }
            }
            //找到最小值的下标后交换
            if (min != i)
            {
                Swap(min, i);
            }
        }
    }
    #endregion

    #region 堆排序 时间复杂度O(n*Log(n))
    private void HeadSort()
    {
        BuildMaxHeap();//创建大顶堆，初始状态看做整体无序
        DebugArray();
        for (int i = array.Length - 1; i > 0; i--)
        {
            Swap(0, i);//把无序区最后一个成员和堆顶调换，使得堆顶进入有序区
            MaxHeapify(0, i);//重新将无序区调整为大顶堆
        }
    }
    //创建大顶堆，原理是数组其实就是按层序遍历得到的二叉树,构建过程就是将每个根节点实现为局部大顶堆
    private void BuildMaxHeap()
    {
        for (int i = array.Length / 2 - 1; i >= 0 ; i--)
        {
            //3 2 1 0,这都是非叶节点
            MaxHeapify(i, array.Length);
        }
    }
    //大顶堆调整过程。比较根节点和它的左右子节点的大小来调换位置，然后将调整后的子节点作为递归的根节点继续局部大顶堆调整直到叶节点为止
    private void MaxHeapify(int cur,int heapSize)
    {
        int left = 2 * cur + 1;//左子节点在数组的位置
        int right = 2 * cur + 2;//右子节点在数组的位置
        int large = cur;//记录此根节点，左子节点，右子节点中最大的位置
        if (left < heapSize && array[left] > array[large])
        {
            large = left;
        }
        if (right < heapSize && array[right] > array[large])
        {
            large = right;
        }
        if (cur != large)//左右子节点有大于根节点的情况
        {
            Swap(cur, large);//局部大顶堆化
            MaxHeapify(large, heapSize);
        }
    }
    #endregion

    #region 插入排序 时间复杂度O(n^2)，效率比冒泡和选择排序更高
    private void InsertSort()
    {
        //每一次循环，都会让0~i的元素完成好排序
        for (int i = 1; i < array.Length; i++)
        {
            //记录的这个值在内循环结束后赋值给插入的成员，内循环其实就是一个找空位成员向后挪的行为
            int value = array[i];
            int index = i - 1;                        
            while (index >= 0 && value < array[index])
            {
                array[index + 1] = array[index];
                index--;
            }
            array[index + 1] = value;
        }
    }
    #endregion

    #region 希尔排序 时间复杂度O(n^(3/2)) 又称缩小增量排序，是插入排序的优化版
    private void ShellSort()
    {
        int gab = array.Length / 2;
        while (gab > 0)
        {
            for (int i = 0; i < gab; i++)
            {
                //这是插入排序的算法
                for (int j = i + gab; j < array.Length; j += gab)
                {
                    int index = j;
                    int value = array[j];
                    while (index - gab >= i && array[index - gab] > value)
                    {
                        array[index] = array[index - gab];
                        index -= gab;
                    }
                    array[index] = value;
                }
            }
            gab /= 2;
        }
    }
    #endregion

    #region 归并排序 时间复杂度n+logn~n
    //1.递归实现：时间复杂度O(n + Log(n)) 递归实现拆分小数组，小数组通过占位的方式排序后再组合
    //2.非递归实现:时间复杂度O(n) 用空间换取了时间
    
    /// <summary>
    /// 递归实现的归并排序
    /// </summary>
    public void MergeSort_recursion()
    {
        MergeSort(array, 0, array.Length - 1);
    }
    private void MergeSort(int[] A, int first, int end)//左开右闭区间[lo,hi)
    {
        if (first < end)
        {
            int mid = (first + end) / 2;
            MergeSort(array, first, mid);
            MergeSort(array, mid + 1, end);
            MergeSortedArray(array, first, mid, end);
        }
    }
    private void MergeSortedArray(int[] array, int first, int mid, int end)
    {
        Debug.LogFormat("first = {0},end = {1}", first, end);
        int[] L = new int[mid - first + 2];
        int[] R = new int[end - mid + 1];
        L[mid - first + 1] = int.MaxValue;
        R[end - mid] = int.MaxValue;

        for (int i = 0; i < mid - first + 1; i++)
        {
            L[i] = array[first + i];
        }

        for (int i = 0; i < end - mid; i++)
        {
            R[i] = array[mid + 1 + i];
        }

        int j = 0;
        int k = 0;
        for (int i = 0; i < end - first + 1; i++)
        {
            if (L[j] <= R[k])
            {
                array[first + i] = L[j];
                j++;
            }
            else
            {
                array[first + i] = R[k];
                k++;
            }
        }
    }
    /// <summary>
    /// 非递归实现的归并排序
    /// </summary>
    public void MergeSort_normal()
    {
        int[] merge = new int[array.Length];
        int P = 0;
        while (true)
        {
            int index = 0;
            //子数组元素的个数
            int num = (int)Mathf.Pow(2, P);
            //一个子数组中的元素个数与数组的一半元素个数比较大小
            //最糟糕的情况最右边的数组只有一个元素
            if (num < array.Length)
            {
                while (true)
                {
                    //最后一个子数组的第一个元素的index与数组index相比较
                    if (index <= array.Length - 1)
                        MergeTwo(array, merge, index, num);
                    else
                        break;
                    index += 2 * num;
                }
            }
            else
                break;
            P++;
        }
    }
    public void MergeTwo(int[] Array, int[] merge, int index, int num)
    {
        Debug.LogFormat("index = {0} num = {1}", index, num);
        // 1
        int left = index;
        int leftMid = left + num - 1;
        //（奇数时）
        //排除middleindex越界
        if (leftMid >= Array.Length)
        {
            leftMid = index;
        }
        //同步化merge数组的index
        int mergeindex = index;
        // 2
        int right;
        int rightMid = leftMid + 1;
        //因为每组长度为num,所以右侧下标是起始下标index + 两倍长度 - 1
        right = index + num + num - 1;        
        //排除最后一个子数组的index越界.
        if (right >= Array.Length - 1)
        {
            right = Array.Length - 1;
        }
        Debug.LogFormat("left = {0} midL = {1} midR = {2} right = {3}", left, leftMid, rightMid, right);
        //排序两个子数组并复制到merge数组
        while (left <= leftMid && rightMid <= right)
        {
            if (Array[left] >= Array[rightMid])
            {
                merge[mergeindex] = Array[rightMid];
                rightMid++;
            }
            else
            {
                merge[mergeindex] = Array[left];
                left++;
            }
            mergeindex++;
            //merge[mergeindex++] = Array[left] >= Array[middleTwo] ? Array[middleTwo++] : Array[left++];
        }
        //两个子数组中其中一个比较完了（Array[middleTwo++] 或Array[left++]），
        //把其中一个数组中剩下的元素复制进merge数组。
        if (left <= leftMid)
        {
            //排除空元素.
            while (left <= leftMid && mergeindex < merge.Length)
                merge[mergeindex++] = Array[left++];           
        } 
        if (rightMid <= right)
        {
            while (rightMid <= right)
                merge[mergeindex++] = Array[rightMid++];            
        }
        DebugOneArray();
            DebugMerge(merge);
        //判断是否合并至最后一个子数组了
        if (right + 1 >= Array.Length)
            Copy(Array, merge);
    }
    public void Copy(int[] Array, int[] merge)
    {
        //一部分排好序的元素替换掉原来Array中的元素
        for (int i = 0; i < Array.Length; i++)
        {
            Array[i] = merge[i];
        }
        Debug.LogError("this is copu func");
        DebugOneArray();
        DebugMerge(merge);
    }

    #endregion

    #region 快速排序 时间复杂度nlogn~n^2
    public void FastSort()
    {
        QuickSort(0, array.Length - 1);
    }
    public void QuickSort(int begin,int end)
    {
        if (begin >= end) return;
        int pivotIndex = QuickSort_Onece(begin, end);

        QuickSort(begin, pivotIndex - 1);
        QuickSort(pivotIndex + 1, end);
    }
    private int QuickSort_Onece(int begin,int end)
    {        
        int pivot = array[begin];   //将首元素作为基准
        while (begin < end)
        {
            Debug.LogFormat("begin {0} end {1}", begin, end);
            while (begin < end && array[end] >= pivot)
            {
                end--;
            }
            Swap(begin, end);
            Debug.Log("right->left");
            DebugOneArray();
            while (begin < end && array[begin] <= pivot)
            {
                begin++;
            }
            Swap(begin, end);
            Debug.Log("left->right");
            DebugOneArray();
        }
        DebugOneArray();
        Debug.Log("find "+ begin);
        return begin;
    }
    #endregion
    private void DebugArray()
    {
        Debug.Log("-------------------------------");
        for (int i = 0; i < array.Length; i++)
        {
            Debug.LogFormat("{0} => {1}", i, array[i]);
        }
    }

    private void DebugOneArray()
    {
        Debug.Log("--------------Array-----------------");
        string s = "";
        for (int i = 0; i < array.Length; i++)
        {
            s += array[i] + " ";
        }
        Debug.Log(s);
    }

    private void DebugMerge(int[] merge)
    {
        Debug.Log("--------------Merge-----------------");
        string s = "";
        for (int i = 0; i < merge.Length; i++)
        {
            s += array[i] + " ";
        }
        Debug.Log(s);
    }
}