using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocksReset : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform OriginalRockLocation;
    OpenSesamePuzzleCheck openSesamePuzzleCheck;

    void Start()
    {
        openSesamePuzzleCheck = FindObjectOfType<OpenSesamePuzzleCheck>();
    }

    // Update is called once per frame
    void Update()
    {
      
        checkPuzzleReset();
    }

    void checkPuzzleReset()
    {
        if (openSesamePuzzleCheck.misplacedRock && openSesamePuzzleCheck.rocksOnAllPlatforms())
        {
           // openSesamePuzzleCheck.resetPuzzle = false;
           

            transform.position = OriginalRockLocation.position;

        }
    }

}
